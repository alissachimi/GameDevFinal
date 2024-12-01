using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    public int score;
    static public Main S;
    public Text scoreText;
    public Slider scoreSlider;
    private int livesRemaining;
    public Image life1Img;
    public Image life2Img;
    public Image life3Img;

    public GameObject beePrefab; // Use a prefab reference
    private GameObject beeInstance;
    public Button startButton;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        S = this;
        scoreSlider.maxValue = 500;
        livesRemaining = 3;
        beeInstance = Instantiate(beePrefab);
        beeInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + " Points";
        scoreSlider.value = score;
    }

    public void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void EnemyCollision(){
        livesRemaining-=1;
        if(livesRemaining==0){
            SwitchScene("End");
            Destroy(life1Img);
        }
        if(livesRemaining==1){
            Destroy(life2Img);
        }
        if(livesRemaining==2){
            Destroy(life3Img);
        }

        beeInstance.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void StartButtonClick(){
        beeInstance.transform.position = startButton.transform.position;
        beeInstance.SetActive(true);
        startButton.gameObject.SetActive(false);
        
    }


}
