using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 10f;
    public float jumpHeight = 3f;
    private float gravity = -25f; 
    
    [Header("Camera Settings")]
    public float mouseSensitivity = 200f;
    public Transform playerCamera; // Drag your camera here!

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet comes out

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ---------------- MOUSE LOOK ----------------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        
        if (playerCamera != null) 
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        transform.Rotate(Vector3.up * mouseX);

        // ---------------- COMBAT (SHOOTING) ----------------
        if (Input.GetButtonDown("Fire1")) 
        {
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }

        // ---------------- MOVEMENT & JUMP ----------------
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMovement = (move * speed) + (Vector3.up * velocity.y);
        controller.Move(finalMovement * Time.deltaTime);

        // ---------------- FALL PENALTY ----------------
        if (transform.position.y < -25f)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.UpdateTimer(-10f); 
            }
            
            GameObject spawn = GameObject.Find("PF_PlayerSpawn");
            if (spawn != null)
            {
                controller.enabled = false; 
                transform.position = spawn.transform.position;
                controller.enabled = true;
                velocity.y = 0f; 
            }
        }
    }
}