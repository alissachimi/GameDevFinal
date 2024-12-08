using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WebRegister : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField passwordInput2;
    public TMP_Text registerErrorText; // Displays error messages to user
    public string nextSceneName = "Start";

    void Start()
    {
    }

    public void OnRegisterButtonClicked()
    {
        string username = usernameInput.text; // Get input from the username field
        string password = passwordInput.text; // Get input from the password field
        string password2 = passwordInput2.text; // Get input from the password field

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2))
        {
            registerErrorText.text = "* Do not leave fields empty. *";
            return;
        }

        // Start the login coroutine
        StartCoroutine(Register(username, password, password2));
    }

    IEnumerator Register(string registerUser, string registerPass, string registerPass2)
    {
        // Create a form object to hold the login data
        WWWForm form = new WWWForm();
        form.AddField("registerUser", registerUser);
        form.AddField("registerPass", registerPass);
        form.AddField("registerPass2", registerPass2);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/game-dev-final-project/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display connection error
                Debug.LogError($"Error: {www.error}");
                registerErrorText.text = "* Connection Error *";
            }
            else
            {
                // Handle the server's response
                string response = www.downloadHandler.text;

                if (int.TryParse(response, out int userId))
                {
                    PlayerPrefs.SetInt("userId", userId);
                    PlayerPrefs.Save();
                    Debug.Log("Registration successful! UserId stored: " + userId);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log($"Registration Failed: {response}");
                    registerErrorText.text = response; // Display error message
                }
            }
        }
    }
}
