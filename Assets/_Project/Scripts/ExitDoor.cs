using UnityEngine;
using UnityEngine.SceneManagement; // We need this to load different scenes!

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Did the player touch the door?
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete! Loading the Map...");
            
            // Unpause the game just in case the Trader menu left it paused
            Time.timeScale = 1f;

            // Load Scene 1 (Your Map Menu)
            // Ensure Scene 1 is added to your Build Settings!
            SceneManager.LoadScene(1);
        }
    }
}