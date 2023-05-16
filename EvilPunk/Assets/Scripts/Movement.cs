using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of movement

    private Rigidbody rb;   // Reference to the Rigidbody component

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }
}



