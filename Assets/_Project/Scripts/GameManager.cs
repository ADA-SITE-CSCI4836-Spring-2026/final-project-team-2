using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float currentTimer = 100f;
    public int currentLayer = 1;
    public float timeOnKillBonus = 0f;

    [Header("Game Over Scene")]
    public string gameOverSceneName; // set from inspector

    [Header("Upgrade Variables")]
    public float bonusTimePerKill = 0f;    // Upgraded via Shop
    public float timerDrainRate = 1f;      // Upgraded via Shop

    [Header("Purchase Tracking")]
    public bool hasBoughtUpgrade1 = false;
    public bool hasBoughtUpgrade2 = false;
    public bool hasBoughtUpgrade3 = false;

    private bool isGameOver = false;

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
        if (isGameOver) return;

        currentTimer -= Time.deltaTime * timerDrainRate;

        if (currentTimer <= 0)
        {
            currentTimer = 0;
            isGameOver = true;

            Debug.Log("GAME OVER! Time ran out.");

            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void UpdateTimer(float amount)
    {
        currentTimer += amount;
    }
}