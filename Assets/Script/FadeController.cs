using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    // Call this to start fading to black
    public void FadeToBlack()
    {
        StartCoroutine(FadeRoutine(0, 1)); // From transparent to opaque
    }

    // Call this to start fading from black
    public void FadeFromBlack()
    {
        StartCoroutine(FadeRoutine(1, 0)); // From opaque to transparent
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color currentColor = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            currentColor.a = Mathf.Lerp(startAlpha, endAlpha, progress);
            fadeImage.color = currentColor;
            yield return null;
        }

        currentColor.a = endAlpha; // Ensure it reaches the final alpha value
        fadeImage.color = currentColor;
    }
}