using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text loginErrorText; // Displays error messages to user
    public string nextSceneName = "Start";

    void Start()
    {
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text; // Get input from the username field
        string password = passwordInput.text; // Get input from the password field

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            loginErrorText.text = "* Do not leave fields empty. *";
            return;
        }

        // Start the login coroutine
        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string loginUser, string loginPass)
    {
        // Create a form object to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("loginUser", loginUser);
        form.AddField("loginPass", loginPass);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/game-dev-final-project/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display connection error
                Debug.LogError($"Error: {www.error}");
                loginErrorText.text = "* Connection Error *";
            }
            else
            {
                // Handle the server's response
                string response = www.downloadHandler.text;

                if (int.TryParse(response, out int userId))
                {
                    PlayerPrefs.SetInt("userId", userId);
                    PlayerPrefs.Save();
                    Debug.Log("Login successful! UserId stored: " + userId);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log($"Login Failed: {response}");
                    loginErrorText.text = response; // Display error message
                }
            }
        }
    }
}
