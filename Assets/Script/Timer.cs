using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CountUpTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to your UI Text element
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime; // Increment elapsed time by the time passed since the last frame

        // Format the time for display (e.g., MM:SS)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}