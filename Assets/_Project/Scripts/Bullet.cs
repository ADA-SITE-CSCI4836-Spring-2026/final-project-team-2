using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float speed = 25f;
    public float lifeTime = 3f;
    public bool isEnemyBullet = false; // Check this box on the enemy's version!

    void Start() 
    {
        Destroy(gameObject, lifeTime); 
    }

    void Update() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // 1. HIT AN ENEMY (If fired by player)
        if (!isEnemyBullet && other.CompareTag("Enemy")) 
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null) 
            {
                enemy.TakeDamage(1f);
                Destroy(gameObject);
            }
        }
        // 2. HIT THE PLAYER (If fired by enemy)
        else if (isEnemyBullet && other.CompareTag("Player")) 
        {
            if (GameManager.Instance != null) 
            {
                GameManager.Instance.UpdateTimer(-5f); // Steal 5 seconds
                Destroy(gameObject);
            }
        }
        // 3. HIT THE WORLD
        else if (other.CompareTag("Floor") || other.CompareTag("Untagged")) 
        {
            // Check if it's not hitting the person who shot it
            if (!other.CompareTag("Player") && !other.CompareTag("Enemy")) 
            {
                Destroy(gameObject);
            }
        }
    }
}