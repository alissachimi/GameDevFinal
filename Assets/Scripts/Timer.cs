using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static public Timer S;

    public float timeRemaining = 240f; // 4 minutes in seconds
    public bool timerIsRunning = false;
    public Text timerText;

    public Canvas timerEndedCanvas;


    void Start()
    {
        S = this;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning && Main.S.paused == false)
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
                Main.S.paused = true;
                timerEndedCanvas.gameObject.SetActive(true);
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
}
