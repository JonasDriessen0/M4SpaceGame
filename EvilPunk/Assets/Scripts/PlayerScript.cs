using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed of movement
    public int maxHP = 150;              // Maximum HP of the player
    public float currentHP;              // Current HP of the player
    public float HPincrease;
    public float flashDuration = 0.1f;   // Duration of each flash
    public Color flashColor = Color.red; // Color to flash the sprite renderers

    private Rigidbody rb;                // Reference to the Rigidbody component
    private bool isFlashing = false;
    private PlayerAnimator playerAnimator;
    public HealthBar healthBar;
    private bool isDelayActive = false;   // Flag to track if delay is active
    private SpriteRenderer[] spriteRenderers; // Array of sprite renderers
    public AudioSource hitAudioSource;   // Audio source for hit sound
    public AudioSource walkingAudioSource; // Audio source for walking sound

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHP = maxHP;  // Set initial HP to maxHP
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = playerObject.GetComponent<PlayerAnimator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Configure walking audio source
        walkingAudioSource.loop = true;
        walkingAudioSource.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        if (currentHP < maxHP)
        {
            currentHP += HPincrease * Time.deltaTime;
        }

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

        // Play walking sound if the player is moving
        if (isMoving)
        {
            if (!walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Play();
            }

            // Adjust the playback speed based on the player's speed
            float playbackSpeed = movement.magnitude / moveSpeed;
            walkingAudioSource.pitch = playbackSpeed;
        }
        // Stop walking sound if the player is not moving
        else if (!isMoving && walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Stop();
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BasicBonk"))
        {
            currentHP -= 35;

            Debug.Log(currentHP);

            if (hitAudioSource != null)
            {
                hitAudioSource.Play();
            }

            StartCoroutine(FlashSpriteRenderers());

            if (currentHP <= 0)
            {
                currentHP = 0;
                StartCoroutine(DelayedSceneSwitch());

            }
        }
        if (other.CompareTag("AdvancedStab"))
        {
            currentHP -= 15;
            Debug.Log(currentHP);

            if (currentHP <= 0)
            {
                currentHP = 0;
                StartCoroutine(DelayedSceneSwitch());
            }
        }
        if (other.CompareTag("ExplosionRadius"))
        {
            currentHP -= 120;
            Debug.Log(currentHP);

            if (currentHP <= 0)
            {
                currentHP = 0;
                StartCoroutine(DelayedSceneSwitch());
            }
        }
    }

    private IEnumerator FlashSpriteRenderers()
    {
        if (isFlashing) // If already flashing, exit the coroutine
            yield break;

        isFlashing = true;

        // Store the initial colors of the sprite renderers
        Color[] initialColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            initialColors[i] = spriteRenderers[i].color;
        }

        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            // Toggle between flashColor and initial colors
            Color targetColor = (elapsedTime % (flashDuration * 2f) < flashDuration) ? flashColor : initialColors[0];

            // Apply the target color to all sprite renderers
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = targetColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Revert sprite renderers back to initial colors
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = initialColors[i];
        }

        isFlashing = false; // Reset the flag when the coroutine is finished
    }


    private IEnumerator DelayedSceneSwitch()
    {
        isDelayActive = true; // Set delay flag to active

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        isDelayActive = false; // Set delay flag to inactive
    }
}
