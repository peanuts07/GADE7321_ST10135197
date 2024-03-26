using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    public BlueBase blueBaseScript;
    public RedBase redBaseScript;
    EnemyStateMachine enemyStateMachine;
    PlayerController playerController;
    public FlagSpawning flagSpawning;
    private int playerScore = 0;
    private int enemyScore = 0;
   
    void Start()
    {
        // Initialize the score text
        UpdateEnemyScoreUI();
        UpdatePlayerScoreUI();
    }

    void Update()
    {
        // Check if the flag has been returned to the base
        if (blueBaseScript != null && blueBaseScript.flagReturned  )
        {
            blueBaseScript.flagReturned = false;

            // Update the score
            UpdatePlayerScore(1); // Increase score by 1 (adjust as needed)

            // Reset the flagReturned bool in the Base script
            ResetGame();
        }
        else if(redBaseScript != null && redBaseScript.enemyFlagReturned)
        {
            redBaseScript.enemyFlagReturned = false;
            UpdateEnemyScore(1);
            ResetGame();
        }
    }

    // Method to update the score
    public void UpdatePlayerScore(int points)
    {
        playerScore += points;
        UpdatePlayerScoreUI();
      
        // Check if the player has scored (e.g., reached a certain score)
        // You can add additional logic here as needed
        if (playerScore >= 5)
        {
            // Call method to handle game over or other actions
            GameOver();
        }
    }
    public void UpdateEnemyScore(int points)
    {
        enemyScore += points;
        UpdateEnemyScoreUI();
      
        // Check if the player has scored (e.g., reached a certain score)
        // You can add additional logic here as needed
        if (enemyScore >= 5)
        {
            // Call method to handle game over or other actions
            GameOver();
        }
    }

    // Method to update the score UI
    void UpdatePlayerScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = "Score: " + playerScore;
        }
    }
    void UpdateEnemyScoreUI()
    {
        if (enemyScoreText != null)
        {
            enemyScoreText.text = "Score: " + enemyScore;
        }
    }

    // Method to handle game over or other actions
    void GameOver()
    {
        // Example: Print game over message
        Debug.Log("Game Over!");

        // Call method to reset the game
       
    }

    // Method to reset the game
    void ResetGame()
    {
        // Reset score and update UI
       // enemyScore = 0;
       // playerScore = 0;
       // UpdateEnemyScoreUI();
       // UpdatePlayerScoreUI();
        if(playerController != null)
        {
            playerController.blueFlagCollected = false;
        }
       if(enemyStateMachine != null)
        {
            enemyStateMachine.redFlagCollected = false;
        }
        if(flagSpawning != null)
        {
            flagSpawning.SpawnFlags();
        }
       


     // players must be respawned 
     // flags must be respawned 
     //theflag collected must be set to false 

    }
}
