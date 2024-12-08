using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class End : MonoBehaviour
{
    public int score;
    public string startTimeString;
    public TimeSpan duration;
    public int userId;
    public TMP_InputField feedbackInput;
    public TMP_Text feedbackErrorText;
    public Button submitFeedbackButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + Main.S.score;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

        score = Main.S.score;
        if (PlayerPrefs.HasKey("GameStartTime"))
        {
            startTimeString = PlayerPrefs.GetString("GameStartTime");
            DateTime startTime;
            // Parse the stored start time
            if (DateTime.TryParse(startTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind, out startTime))
            {
                DateTime endTime = DateTime.Now;
                duration = endTime - startTime;

                Debug.Log($"Game Duration: {duration.TotalSeconds} seconds");
            }
        }
        if (PlayerPrefs.HasKey("userId")){
            userId = PlayerPrefs.GetInt("userId");
        }
        StartCoroutine(History(userId, startTimeString, score, duration.TotalSeconds));
    }

    public void OnFeedbackButtonClicked()
    {   
        string feedback = feedbackInput.text;

        if (string.IsNullOrEmpty(feedback))
        {
            feedbackErrorText.text = "* Please enter feedback. *";
            return;
        }

        Debug.Log($"User Feedback: {feedback}");

        // Start the feedback coroutine
        StartCoroutine(Feedback(userId, startTimeString, feedback));
    }

    IEnumerator History(int historyId, string historyTime, int historyScore, double historyDuration)
    {
        int historyDurationFormatted = ((int)historyDuration);
        // Create a form object to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("historyId", historyId);
        form.AddField("historyScore", historyScore);
        form.AddField("historyTime", historyTime);
        form.AddField("historyDuration", historyDurationFormatted);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/game-dev-final-project/history.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display connection error
                Debug.LogError($"Error: {www.error}");
            }
        }
    }

    IEnumerator Feedback(int feedbackId, string feedbackTime, string feedbackText)
    {
        // Create a form object to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("feedbackId", feedbackId);
        form.AddField("feedbackTime", feedbackTime);
        form.AddField("feedbackText", feedbackText);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/game-dev-final-project/feedback.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display connection error
                Debug.LogError($"Error: {www.error}");
                feedbackErrorText.text = "* Connection Error *";
            }
            else
            {
                CloseFeedbackForm();
            }
        }
    }

    void CloseFeedbackForm(){
        submitFeedbackButton.gameObject.SetActive(false);
        feedbackInput.gameObject.SetActive(false);
        feedbackErrorText.gameObject.SetActive(false);
        return;
    }
}
