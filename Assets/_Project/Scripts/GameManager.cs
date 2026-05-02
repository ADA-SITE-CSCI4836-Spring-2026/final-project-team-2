using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public float currentTimer = 100f;
    public int currentLayer = 1;
    
    [Header("Timer State")]
    public bool isTimerRunning = false; // NEW: Controls if the clock is ticking

    [Header("Upgrade Variables")]
    public float bonusTimePerKill = 0f;    
    public float timerDrainRate = 1f;      

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
        // If the timer is told to pause (like in the Map), stop doing the math below
        if (!isTimerRunning) return;

        currentTimer -= Time.deltaTime * timerDrainRate;

        if (currentTimer <= 0)
        {
            currentTimer = 0;
            Debug.Log("GAME OVER! Time ran out. Returning to Main Menu...");
            
            ResetGameStats();
            SceneManager.LoadScene(0);
        }
    }
    
    public void UpdateTimer(float amount) 
    { 
        currentTimer += amount; 
    }

    private void ResetGameStats()
    {
        currentTimer = 100f;
        currentLayer = 1;
        isTimerRunning = false; // Freeze the clock on death
        
        bonusTimePerKill = 0f;
        timerDrainRate = 1f;
        
        hasBoughtUpgrade1 = false;
        hasBoughtUpgrade2 = false;
        hasBoughtUpgrade3 = false;
    }
}