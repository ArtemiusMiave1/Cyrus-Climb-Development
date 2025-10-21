using UnityEngine;
using TMPro; // Don't forget this for TextMeshPro

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startTime = 60f; // Starting time in seconds
    private float currentTime;
    private bool timerActive = true;
    public bool locked;


    public static bool locked2;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if ( locked == false)
            {

                timerActive = true;
                locked = true;
            }
        }

            if (timerActive && locked2 == false)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerActive = false;
                // Add any logic for when the timer ends (e.g., game over, level complete)
                Debug.Log("Timer Ended!");
            }

            DisplayTime(currentTime);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}