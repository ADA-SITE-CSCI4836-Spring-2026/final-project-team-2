using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 1. Freeze the timer while we are on the map!
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isTimerRunning = false;
        }
    }

    public void OnNodeClicked()
    {
        Debug.Log("Loading Gameplay Level...");
        
        // 2. Start the timer right before we jump into the level
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isTimerRunning = true;
        }
        
        SceneManager.LoadScene(2); 
    }
}