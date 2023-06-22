using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of movement
    public int maxHP = 100;          // Maximum HP of the player
    private int currentHP;           // Current HP of the player

    private Rigidbody rb;   // Reference to the Rigidbody component
    private PlayerAnimator playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;  // Set initial HP to maxHP
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicBonk"))
        {
            // Reduce HP by 30
            currentHP -= 30;
            Debug.Log(currentHP);

            // Check if HP drops below 0
            if (currentHP <= 0)
            {
                currentHP = 0;
            }
        }
        if (other.CompareTag("AdvancedStab"))
        {
            // Reduce HP by 30
            currentHP -= 10;
            Debug.Log(currentHP);

            // Check if HP drops below 0
            if (currentHP <= 0)
            {
                currentHP = 0;
            }
        }
    }
}
