using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float speed = 4f;
    public float stoppingDistance = 10f;
    
    public GameObject enemyBulletPrefab;
    public float fireRate = 1.5f;
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
            // Lock rotation to Y axis so they don't lean
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // Check distance
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > stoppingDistance)
            {
                // Move closer if too far
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                // Stand still and shoot if close enough
                fireTimer += Time.deltaTime;
                if (fireTimer >= fireRate)
                {
                    Shoot();
                    fireTimer = 0f;
                }
            }
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            Vector3 spawnPos = transform.position + (transform.forward * 1.5f);
            Instantiate(enemyBulletPrefab, spawnPos, transform.rotation);
        }
    }
}