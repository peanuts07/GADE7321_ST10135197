using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateMachine : MonoBehaviour
{
    public enum PlayerState
    {
        Collecting,
        Chasing,
        Returning,
        Attacking
    }

    private PlayerState currentState;
   public Transform player;
    public Transform redFlag;
    public Transform blueFlag;
    public Transform basePoint;
    NavMeshAgent agent;
    public bool redFlagCollected = false;
    public PlayerController playerController; // Reference to the PlayerController script
    public GameObject projectilePrefab;
    public Transform firePoint;
    public LayerMask playerLayer;

    public float fireRate = 1f;
    private float fireCooldown = 0f;
    private float projectileSpeed = 10f;
    public float detectionRange = 5f;
    private bool inRange=false;
    public Transform blueSP;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        redFlag = GameObject.FindGameObjectWithTag("RedFlag").transform;
        blueFlag = GameObject.FindGameObjectWithTag("BlueFlag").transform;
        basePoint = GameObject.FindGameObjectWithTag("Base").transform;


        // Set initial state
        SetState(PlayerState.Collecting);
    }

    void Update()
    {
        PlayerRange();
        if (redFlagCollected == true)
        {
            SetState(PlayerState.Returning);
        }
        else if (playerController != null && playerController.blueFlagCollected == true) 
        {
            if (inRange == false)
            {
                SetState(PlayerState.Chasing);
            }
            else if(inRange==true)
            {
                SetState(PlayerState.Attacking);
            }
            
        }
        else
        {
            SetState(PlayerState.Collecting);
        }
        // Perform state-specific behavior
        PerformStateBehavior();
    }

    private void SetState(PlayerState newState)
    {
        currentState = newState;
    }

    private void PerformStateBehavior()
    {
        switch (currentState)
        {
            case PlayerState.Collecting:
                // Chase red flag if player doesn't have blue flag
                if (!player.GetComponent<PlayerController>().blueFlagCollected)
                    CollectFlag();
                    //Chase(redFlag);
                break;

            case PlayerState.Chasing:
                // Chase player to make them drop blue flag
                Chase( );
                break;

            case PlayerState.Returning:
                // Return red flag to base
                ReturnToBase();
                break;

            case PlayerState.Attacking:
                // Attempt to collect dropped flag
                FireAtPlayer();
                break;
        }
    }
    private void CollectFlag()
    {
        agent.SetDestination(redFlag.position);
    }
    private void Chase() // done
    {
        // Implement chase behavior here
        agent.SetDestination(player.position);
        Debug.Log("Chasing ");
    }

    private void ReturnToBase()
    {
        agent.SetDestination(basePoint.position);
        // Implement returning behavior here
        Debug.Log("Returning   to base");
    }

   
    void FireAtPlayer()
    {
        // Raycast to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, playerLayer);

        // Check if the raycast hits the player
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Calculate direction towards the player
            Vector2 direction = (hit.collider.transform.position - transform.position).normalized;

            // Instantiate projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            // Rotate projectile to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Add force to the projectile to shoot towards the player
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RedFlag"))
        {
            collision.transform.SetParent(this.transform);
            redFlagCollected = true;
        }
        if (collision.gameObject.CompareTag("BlueFlag"))
        {
            collision.transform.position = blueSP.position;
            collision.transform.SetParent(null);
           
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player"))
        {
           
            if (playerController != null)
            {
                playerController.blueFlagCollected = false;
            }
        }

    }
    private void PlayerRange()
    {
        if (player != null)
        {
            // Calculate the squared distance between the AI and the player
            float sqrDistanceToPlayer = (player.position - transform.position).sqrMagnitude;

            // Calculate the squared detection range
            float sqrDetectionRange = detectionRange * detectionRange;

            // Check if the squared distance is less than or equal to the squared detection range
            if (sqrDistanceToPlayer <= sqrDetectionRange)
            {
                inRange = true;
                // Player is in range
                Debug.Log("Player is in range!");
            }
            else
            {
                inRange = false; 
                // Player is out of range
                Debug.Log("Player is out of range!");
            }
        }
    }
}
