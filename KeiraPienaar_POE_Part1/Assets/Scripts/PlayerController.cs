using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    public bool blueFlagCollected = false;
    public Transform playerBasePoint; // Reference to the player's base point
    public Transform redSP;
    public EnemyStateMachine fsm;
   // public Transform blueFlag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Horizontal movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Vertical movement
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, verticalInput * moveSpeed);
    }

    void FixedUpdate()
    {
        // Flip the player sprite based on movement direction
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void SpawnEnemyFlagAtBase(Transform enemyFlagPrefab)
    {
        if (playerBasePoint != null && enemyFlagPrefab != null)
        {
            // Instantiate the enemy flag prefab at the player's base point
            Instantiate(enemyFlagPrefab, playerBasePoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Base point or enemy flag prefab is not set!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (fsm != null)
            {
               fsm.redFlagCollected = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlueFlag"))
        {
            collision.transform.SetParent(this.transform);
            blueFlagCollected = true;
        }
        if (collision.gameObject.CompareTag("RedFlag"))
        {
            collision.transform.position = redSP.position;
            collision.transform.SetParent(null);
           
        }
        
    }
}
