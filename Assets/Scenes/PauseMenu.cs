using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // --- NEW CODE: Singleton Pattern ---
    // This static variable holds the one true instance of our PauseMenu
    public static PauseMenu Instance;

    void Awake()
    {
        // Check if an Instance already exists
        if (Instance == null)
        {
            // If not, this becomes the Instance
            Instance = this;
            
            // Tell Unity to keep this GameObject alive when loading new scenes
            // Note: This only works if the GameObject is at the very root of your Hierarchy!
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // If an Instance already exists, destroy this duplicate
            Destroy(gameObject); 
        }
    }
    // -----------------------------------

    public static bool GameIsPaused = false;

    [Tooltip("Drag your Pause Menu UI Panel here")]
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;          
        GameIsPaused = false;         
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  
        Time.timeScale = 0f;          
        GameIsPaused = true;          
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}