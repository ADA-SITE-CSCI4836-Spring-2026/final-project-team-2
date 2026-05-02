using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Did the player touch the door?
        if (other.CompareTag("Player"))
        {
            // 1. Unpause the game just in case
            Time.timeScale = 1f;

            if (GameManager.Instance != null)
            {
                // 2. Check if this is the final level (Layer 4)
                if (GameManager.Instance.currentLayer >= 4)
                {
                    Debug.Log("GAME WON! Saving Score...");
                    
                    // Save the score using the remaining time
                    SaveHighScore(GameManager.Instance.currentTimer);

                    // Destroy the GameManager (The run is over)
                    Destroy(GameManager.Instance.gameObject);
                    GameManager.Instance = null;

                    // Load the Leaderboard (Scene 6)
                    SceneManager.LoadScene(6);
                }
                else
                {
                    Debug.Log("Level Complete! Advancing Layer...");
                    
                    // Not the final level? Advance the layer and load the map.
                    GameManager.Instance.currentLayer++;
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    // --- LEADERBOARD SAVE LOGIC ---
    private void SaveHighScore(float newScore)
    {
        // We will keep track of the Top 5 best times
        for (int i = 1; i <= 5; i++)
        {
            // Get the saved score for this position (Default to 0 if it doesn't exist)
            float savedScore = PlayerPrefs.GetFloat("HighScore_" + i, 0f);

            // If our new score is better than this slot...
            if (newScore > savedScore)
            {
                // Push all the lower scores down one slot so we don't delete them
                for (int j = 5; j > i; j--)
                {
                    float previousScore = PlayerPrefs.GetFloat("HighScore_" + (j - 1), 0f);
                    PlayerPrefs.SetFloat("HighScore_" + j, previousScore);
                }

                // Save our new high score in this slot!
                PlayerPrefs.SetFloat("HighScore_" + i, newScore);
                PlayerPrefs.Save(); // Forces Unity to write to the hard drive immediately
                break; // Stop looking, we placed our score!
            }
        }
    }
}