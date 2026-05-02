using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the Layer 7 Boss Arena: A wave-based survival Last Stand.
/// </summary>
public class LastStandManager : MonoBehaviour
{
    [Header("Spawner Setup")]
    [Tooltip("Assign Tier 3 enemy prefabs here (Melee, Range, Kamikaze, Turret)")]
    public GameObject[] enemyPrefabs;
    [Tooltip("Assign empty GameObjects placed around the arena as spawn points")]
    public Transform[] spawnPoints;

    [Header("Wave Configuration")]
    [Tooltip("Total number of waves the player must survive.")]
    public int totalWaves = 5;
    [Tooltip("Time delay before the next wave spawns.")]
    public float timeBetweenWaves = 8f;
    [Tooltip("Base number of enemies to spawn in the first wave.")]
    public int baseEnemiesPerWave = 3;
    [Tooltip("How many extra enemies are added each consecutive wave.")]
    public int enemyScalingPerWave = 2;

    private int currentWave = 0;
    private int activeEnemyCount = 0;
    private bool isSpawningFinished = false;

    void Start()
    {
        // Start the Last Stand sequence as soon as the scene loads
        StartCoroutine(WaveRoutine());
    }

    /// <summary>
    /// Coroutine handling the timing and spawning of enemy waves.
    /// </summary>
    private IEnumerator WaveRoutine()
    {
        Debug.Log("Last Stand Initiated!");

        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log($"Spawning Wave {currentWave} of {totalWaves}");

            int enemiesToSpawn = baseEnemiesPerWave + (currentWave * enemyScalingPerWave);
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(0.5f); // Stagger spawns slightly
            }

            // Wait for the next wave, or stop if it's the last wave
            if (currentWave < totalWaves)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        isSpawningFinished = true;
        Debug.Log("All waves spawned! Defeat remaining enemies to win.");
    }

    /// <summary>
    /// Spawns a random enemy at a random spawn point.
    /// </summary>
    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        // Pick random enemy and random location
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate and track the enemy
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        activeEnemyCount++;

        // NOTE: You will need to hook this up to your actual Enemy health script!
        // Example: newEnemy.GetComponent<EnemyHealth>().OnDeath += HandleEnemyDeath;
    }

    /// <summary>
    /// Call this method from your Enemy script when an enemy dies.
    /// </summary>
    public void HandleEnemyDeath()
    {
        activeEnemyCount--;

        // Check Win Condition: All waves spawned and all enemies dead
        if (isSpawningFinished && activeEnemyCount <= 0)
        {
            TriggerWinCondition();
        }
    }

    private void TriggerWinCondition()
    {
        Debug.Log("YOU SURVIVED THE CHRONO-DEBT! WINNER!");
        // TODO: Tell your GameManager to stop the ChronoTimer.
        // TODO: Save the current ChronoTimer value as the High Score.
        // TODO: Load the Victory/Leaderboard Screen.
    }
}