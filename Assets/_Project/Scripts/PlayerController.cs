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

        // ---------------- MOVEMENT & JUMP ----------------
        // 1. Keep player glued to floor if not jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        // 2. Calculate Walking
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // 3. Check for Jump
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Physics formula: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Calculate Gravity
        velocity.y += gravity * Time.deltaTime;

        // 5. COMBINE ALL MOVEMENT INTO ONE CALL
        // Note: move * speed handles horizontal, Vector3.up * velocity.y handles vertical
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
                
                // Reset velocity so you don't keep falling speed after teleporting
                velocity.y = 0f; 
            }
        }
    }
}