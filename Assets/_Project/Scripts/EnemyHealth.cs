using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public float health = 1f;
    public float timeReward = 5f; // Base time earned back on kill

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
        if (GameManager.Instance != null)
        {
            float finalReward = timeReward;

            // Apply Upgrade 3: Double Base Rewards
            if (GameManager.Instance.hasBoughtUpgrade3)
            {
                finalReward *= 2f; 
            }

            // Apply Upgrade 1: Flat Bonus Time
            finalReward += GameManager.Instance.bonusTimePerKill;

            GameManager.Instance.UpdateTimer(finalReward);
        }
        
        Destroy(gameObject);
    }
}