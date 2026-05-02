using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float currentTimer = 100f;
    public int currentLayer = 1;

    void Awake() 
    { 
        // This ensures only one GameManager exists and it survives scene loads
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Decrease timer by 1 real-world second every second
        currentTimer -= Time.deltaTime;

        // Check for Game Over
        if (currentTimer <= 0)
        {
            currentTimer = 0;
            Debug.Log("GAME OVER! Time ran out.");
            // Later we will load the Menu scene here
        }
    }
    
    public void UpdateTimer(float amount) 
    { 
        currentTimer += amount; 
    }
}