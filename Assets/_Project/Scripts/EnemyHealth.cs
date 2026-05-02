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
        if (GameManager.Instance != null)
        {
            float finalReward = timeReward + GameManager.Instance.timeOnKillBonus;
            GameManager.Instance.UpdateTimer(finalReward);

            Debug.Log("Enemy killed. Time added: " + finalReward);
        }

        Destroy(gameObject);
    }
}