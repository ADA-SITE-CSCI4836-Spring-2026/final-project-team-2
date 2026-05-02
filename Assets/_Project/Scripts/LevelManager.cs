using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Core Prefabs")]
    public GameObject playerPrefab;
    
    void Start()
    {
        // 1. Find the Player Spawn point in the current level
        GameObject spawnPoint = GameObject.Find("PF_PlayerSpawn");
        
        if (spawnPoint != null && playerPrefab != null)
        {
            // 2. Spawn the player (and their attached camera!) exactly at that spot
            Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogError("Could not find PF_PlayerSpawn in the level, or Player Prefab is missing!");
        }
    }
}