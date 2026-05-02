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

            // Upgrade 3: Double base reward
            if (GameManager.Instance.hasBoughtUpgrade3)
            {
                finalReward *= 2f;
            }

            // Upgrade 1: Add bonus time per kill
            finalReward += GameManager.Instance.bonusTimePerKill;

            GameManager.Instance.UpdateTimer(finalReward);

            Debug.Log("Enemy killed. Time added: " + finalReward);
        }

        Destroy(gameObject);
    }
}