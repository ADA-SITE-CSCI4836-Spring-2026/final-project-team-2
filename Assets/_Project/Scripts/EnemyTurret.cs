using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject enemyBulletPrefab; 
    public float fireRate = 2f;
    private float fireTimer = 0f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Look exactly at the player
            transform.LookAt(player);

            // Timer logic for shooting
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            // Spawns a bullet just in front of the turret, pointing at the player
            Vector3 spawnPos = transform.position + (transform.forward * 1.5f);
            Instantiate(enemyBulletPrefab, spawnPos, transform.rotation);
        }
    }
}