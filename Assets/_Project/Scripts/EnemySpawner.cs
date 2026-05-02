//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
//    [Header("Enemies")]
//    public GameObject[] enemyPrefabs;

//    [Header("Spawn Points")]
//    public Transform[] spawnPoints;

//    private bool hasSpawned = false;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (hasSpawned) return;

//        if (other.CompareTag("Player"))
//        {
//            SpawnEnemies();
//            hasSpawned = true;
//        }
//    }

//    private void SpawnEnemies()
//    {
//        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
//        {
//            Debug.LogError("Enemy prefabs missing!");
//            return;
//        }

//        if (spawnPoints == null || spawnPoints.Length == 0)
//        {
//            Debug.LogError("Spawn points missing!");
//            return;
//        }

//        foreach (Transform spawnPoint in spawnPoints)
//        {
//            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

//            Instantiate(
//                enemyPrefabs[randomEnemyIndex],
//                spawnPoint.position,
//                spawnPoint.rotation
//            );
//        }
//    }
//}