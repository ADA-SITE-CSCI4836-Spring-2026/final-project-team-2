using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Did the player touch the door?
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete! Advancing Layer...");
            
            // 1. Unpause the game just in case the Trader menu left it paused
            Time.timeScale = 1f;

            // 2. Increase the Layer by 1 in the GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.currentLayer++;
            }

            // 3. Load Scene 1 (The Map Menu)
            SceneManager.LoadScene(1);
        }
    }
}