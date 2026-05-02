using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public float health = 1f;
    public float timeReward = 5f; // Earning time back on kill

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die() 
    {
        // Add time reward to the game clock
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateTimer(timeReward);
        }
        
        Destroy(gameObject);
    }
}