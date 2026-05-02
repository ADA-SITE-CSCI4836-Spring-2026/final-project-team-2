using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float speed = 5f;
    private Transform player;

    void Start()
    {
        // Find the player automatically when the enemy spawns
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
            // Calculate direction to player
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Look at the player (Y remains unchanged so they don't tilt into the floor)
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            
            // Move toward the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}