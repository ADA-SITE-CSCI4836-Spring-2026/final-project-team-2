using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start() 
    {
        // Cleanup if it misses to save memory
        Destroy(gameObject, lifeTime); 
    }

    void Update() 
    {
        // Move the bullet forward based on its own rotation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            // We will add damage logic here in Phase 2
            Destroy(gameObject);
        }
    }
}