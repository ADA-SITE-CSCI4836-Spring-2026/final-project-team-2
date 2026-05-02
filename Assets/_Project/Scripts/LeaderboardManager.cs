using UnityEngine;
using UnityEngine.UI; // Needed to modify Text
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public Text scoresTextDisplay; // Drag your Scores_Text here in the inspector

    void Start()
    {
        // Unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisplayScores();
    }

    void DisplayScores()
    {
        string boardText = "";

        // Loop through the Top 5 slots
        for (int i = 1; i <= 5; i++)
        {
            // Grab the score from the hard drive
            float score = PlayerPrefs.GetFloat("HighScore_" + i, 0f);
            
            if (score > 0)
            {
                // Format it cleanly (e.g., "1. Time: 145.2s")
                boardText += i + ". Time: " + score.ToString("F1") + "s\n";
            }
            else
            {
                boardText += i + ". ---\n";
            }
        }

        // Apply it to the UI text
        if (scoresTextDisplay != null)
        {
            scoresTextDisplay.text = boardText;
        }
    }

    public void ReturnToMenu()
    {
        // Loads the Main Menu (Scene 0)
        SceneManager.LoadScene(0);
    }
}