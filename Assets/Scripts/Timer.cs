using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static public Timer S;

    public float timeRemaining = 240f; // 4 minutes in seconds
    public bool timerIsRunning = false;
    public Text timerText; // Assign a UI Text element in the Inspector

    void Start()
    {
        S = this;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                //TimerEnded();
            }
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);

            // Format the time as MM:SS
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }

    // void TimerEnded()
    // {
    //     Main.S.levelFailed();
    // }
}
