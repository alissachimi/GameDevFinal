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
    private bool firstClick = true;
    
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
        if(livesRemaining==1 && life1Img){
            SwitchScene("End");
            Destroy(life1Img);
        }
        if(livesRemaining==2 && life2Img){
            Destroy(life2Img);
        }
        if(livesRemaining==3 && life3Img){
            Destroy(life3Img);
        }

        beeInstance.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public void StartButtonClick(){
        
        // remove a life here instead of in EnemyCollision to avoid multiple lives getting lost error
        if(!firstClick){
            livesRemaining-=1;
        } else {
            firstClick = false;
        }

        // recreate bee at the location of the start button
        beeInstance.transform.position = Vector3.up;
        beeInstance.SetActive(true);
        startButton.gameObject.SetActive(false);
        
    }


}
