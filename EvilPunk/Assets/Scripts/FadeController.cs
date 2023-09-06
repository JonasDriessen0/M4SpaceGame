using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        // Set initial alpha to 0 (fully transparent)
        fadeImage.canvasRenderer.SetAlpha(0f);

        fadeImage.raycastTarget = false; // Disable raycast target on the black image
    }

    public void StartFadeEffect()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        fadeImage.CrossFadeAlpha(1f, fadeDuration, false); // Fade to alpha 1 (fully opaque)

        yield return new WaitForSeconds(fadeDuration); // Wait for the fade duration

        // Fade effect complete
    }
}
