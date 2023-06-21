using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of movement

    private Rigidbody rb;   // Reference to the Rigidbody component
    private PlayerAnimator playerAnimator;  // Reference to the PlayerAnimator script

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = playerObject.GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed;

        // Apply movement to the Rigidbody
        rb.velocity = movement;

        // Check if the player is moving
        bool isMoving = movement.magnitude > 0f;

        // Set the "IsMoving" parameter in the PlayerAnimator script
        if (playerAnimator != null)
        {
            playerAnimator.SetMoving(isMoving);
        }
    }
}