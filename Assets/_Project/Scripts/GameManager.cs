using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public float currentTimer = 100f;
    public int currentLayer = 1;

    [Header("Upgrade Variables")]
    public float bonusTimePerKill = 0f;    // Upgraded via Shop
    public float timerDrainRate = 1f;      // Upgraded via Shop

    [Header("Purchase Tracking")]
    public bool hasBoughtUpgrade1 = false;
    public bool hasBoughtUpgrade2 = false;
    public bool hasBoughtUpgrade3 = false;

    void Awake() 
    { 
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
        // Timer decreases based on the drain rate (Upgrade 2 slows this down)
        currentTimer -= Time.deltaTime * timerDrainRate;

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