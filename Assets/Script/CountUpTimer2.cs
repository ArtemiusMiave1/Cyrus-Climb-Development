using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CountUpTimer2 : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to your UI Text element
    private float elapsedTime = 0f;
    public bool locked;


    public static bool locked2 = true;

    void Update()
    {

        if (Input.anyKeyDown)
        {
            if (locked == false)
            {

                locked2 = false;
                locked = true;
            }
        }

        if (locked2 == false)
        { 
        elapsedTime += Time.deltaTime; // Increment elapsed time by the time passed since the last frame

        // Format the time for display (e.g., MM:SS)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    }
}