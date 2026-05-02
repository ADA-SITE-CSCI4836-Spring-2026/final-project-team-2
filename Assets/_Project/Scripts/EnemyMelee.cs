using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float speed = 5f;
    public float damageAmount = 10f; 
    public float attackCooldown = 1.5f;
    
    private Transform player;
    private float lastAttackTime;

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
            // Stay on the floor: lock Y position
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(targetPos);
            
            // Move toward player
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    // We use OnTriggerStay because the colliders are now set to "Is Trigger"
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                if (GameManager.Instance != null) 
                {
                    GameManager.Instance.UpdateTimer(-damageAmount);
                    lastAttackTime = Time.time;
                    Debug.Log("Melee Hit! Time Lost.");
                }
            }
        }
    }
}