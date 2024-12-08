using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class WebLevel : MonoBehaviour
{
    public TMP_Text lastCompletedLevelText;
    // Start is called before the first frame update
    void Start()
    {
        int userId = PlayerPrefs.GetInt("userId");
        StartCoroutine(GetLevel(userId));
    }

    IEnumerator GetLevel(int userId)
    {
        // Create a form object to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("getLevelId", userId);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/game-dev-final-project/level.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display connection error
                Debug.LogError($"Error: {www.error}");
            }
            else
            {
                // Handle the server's response
                string response = www.downloadHandler.text;

                if (int.TryParse(response, out int lastCompletedLevel))
                {
                    if (lastCompletedLevel > 0){
                        lastCompletedLevelText.gameObject.SetActive(true);
                        lastCompletedLevelText.text = "Last Completed Level: " + lastCompletedLevel.ToString();
                    }
                    else {
                        lastCompletedLevelText.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
