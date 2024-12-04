using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    public int score;
    static public Main S;
    public Text scoreText;
    public Text levelText;
    public Slider scoreSlider;
    private int livesRemaining;
    public Image life1Img;
    public Image life2Img;
    public Image life3Img;

    public GameObject beePrefab; // Use a prefab reference
    private GameObject beeInstance;
    private Button startButton;
    private bool firstClick = true;

    // levels variables
    private int maxBalloonsForCurLevel;
    private int[] totalNumBalloons = {32, 44, 54};
    public int numBalloonsPopped = 0;
    private int curLevel = 0;
    private int lastLevel = 2;
    public GameObject[] levelObjects;

    public Button [] startButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        S = this;

        // max score would be total number of balloons * 10 pts per balloon popped
        scoreSlider.maxValue = totalNumBalloons.Sum() * 10;

        livesRemaining = 3;
        beeInstance = Instantiate(beePrefab);
        beeInstance.SetActive(false);

        startButton = startButtons[0];
        maxBalloonsForCurLevel = totalNumBalloons[0];
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + " Points";
        scoreSlider.value = score;

        if(maxBalloonsForCurLevel == numBalloonsPopped){
            IncreaseLevel();
        }
    }

    public void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void EnemyCollision(){
        if(livesRemaining==1 && life1Img){
            SwitchScene("End");
            life1Img.gameObject.SetActive(false);
        }
        if(livesRemaining==2 && life2Img){
            life2Img.gameObject.SetActive(false);
        }
        if(livesRemaining==3 && life3Img){
            life3Img.gameObject.SetActive(false);
        }

        beeInstance.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    private void IncreaseLevel() {

        // end game if we've done all of the levels
        if(curLevel == lastLevel){
            SceneManager.LoadScene("End");
            return;
        }

        // get rid of old level and put in new one
        levelObjects[curLevel].SetActive(false);
        curLevel+=1;

        levelObjects[curLevel].SetActive(true);
        beeInstance.SetActive(false);

        // curlevel is 0 based; update UI
        levelText.text = "Level: " + (curLevel+1);

        // set all vars to info for new level
        startButton = startButtons[curLevel];
        startButton.gameObject.SetActive(true);
        maxBalloonsForCurLevel = totalNumBalloons[curLevel];
        numBalloonsPopped = 0;
        firstClick = true;
        livesRemaining = 3;

        // recreate life pictures if needed
        life1Img.gameObject.SetActive(true);
        life2Img.gameObject.SetActive(true);
        life3Img.gameObject.SetActive(true);
        
    }

    public void StartButtonClick(){
        
        // remove a life here instead of in EnemyCollision to avoid multiple lives getting lost error
        if(!firstClick){
            livesRemaining-=1;
        } else {
            firstClick = false;
        }

        // recreate bee at the location of the start button
        beeInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        beeInstance.SetActive(true);
        startButton.gameObject.SetActive(false);
        
    }


}
