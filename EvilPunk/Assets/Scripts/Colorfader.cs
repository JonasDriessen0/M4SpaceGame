using UnityEngine;

public class ColorFader : MonoBehaviour
{
    public float fadeDuration = 1.0f;  // Duration of the fade in seconds
    public Color targetColor = Color.red;  // Color to fade to

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private float timer;
    private bool fading;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        fading = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartFade();
        }

        if (fading)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / fadeDuration);

            // Lerp between startColor and targetColor
            Color currentColor = Color.Lerp(startColor, targetColor, t);
            spriteRenderer.color = currentColor;

            if (t >= 1.0f)
            {
                // Finished fading, reset variables
                fading = false;
                timer = 0.0f;
            }
        }
    }

    public void StartFade()
    {
        // Start the fade
        fading = true;
        startColor = spriteRenderer.color;
    }
}