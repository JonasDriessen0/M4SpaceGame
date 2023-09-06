using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;         // Reference to the UI Image component used for fading
    public AudioSource audioSource; // Reference to the AudioSource component to play the sound
    public PlayerScript playerScript; // Reference to the PlayerScript component

    public float fadeDuration = 2f; // Duration of the fade-in and fade-out effects in seconds

    private float currentAlpha = 1f; // Current alpha value of the fade image
    private float fadeTimer = 0f;    // Timer to track the progress of the fade effect
    private bool isFadingOut = false; // Flag to indicate if we are fading out
    private bool isBlack = false;    // Flag to indicate if the screen is black

    void Start()
    {
        fadeImage.gameObject.SetActive(true); // Make sure the fade image is active
        fadeImage.color = Color.black;         // Set the initial color of the fade image to black
    }

    void Update()
    {
        // Check if the fade effect is still in progress
        if (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime; // Update the timer

            // Calculate the current alpha value based on the progress of the fade effect
            currentAlpha = isFadingOut ? (fadeTimer / fadeDuration) : 1f - (fadeTimer / fadeDuration);

            // Apply the current alpha value to the fade image
            fadeImage.color = new Color(0f, 0f, 0f, currentAlpha);
        }
        else
        {
            // If the fade effect is complete and we are fading out
            if (isFadingOut)
            {
                if (!isBlack)
                {
                    fadeImage.color = Color.black; // Set the fade image to black
                    isBlack = true;                // Set the screen to black
                    fadeImage.gameObject.SetActive(false); // Disable the fade image
                }
            }
        }

        // Check if the player's currentHP reaches 0
        if (playerScript != null && !isFadingOut)
        {
            float currentHP = playerScript.currentHP;
            if (currentHP <= 0)
            {
                // Start fading out and play the audio
                isFadingOut = true;
                fadeTimer = 0f;
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
