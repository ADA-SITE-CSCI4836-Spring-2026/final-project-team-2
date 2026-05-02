using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Core Prefabs")]
    public GameObject playerPrefab;
    
    [Header("Enemy Spawning")]
    public GameObject[] enemyPrefabs; // This creates a list of enemies we can spawn

    void Start()
    {
        // ---------------- 1. SPAWN THE PLAYER ----------------
        GameObject spawnPoint = GameObject.Find("PF_PlayerSpawn");
        
        if (spawnPoint != null && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogError("Could not find PF_PlayerSpawn, or Player Prefab is missing!");
        }

        // ---------------- 2. SPAWN THE ENEMIES ----------------
        // Find every single object in the scene that has the "EnemySpawn" tag
        GameObject[] enemySpawnMarkers = GameObject.FindGameObjectsWithTag("EnemySpawn");

        // If we have enemies in our list, loop through every marker and spawn one
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            foreach (GameObject marker in enemySpawnMarkers)
            {
                // Pick a random number between 0 and the total number of enemies in our list
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                
                // Spawn that random enemy exactly at the red marker's position
                Instantiate(enemyPrefabs[randomIndex], marker.transform.position, marker.transform.rotation);
            }
        }
    }
}