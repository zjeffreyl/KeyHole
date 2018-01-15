using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    [SerializeField]
    private Text Score;
    public int totalPoints;
    public int defaultFontSize = 35;

    [SerializeField]
    private Text HighScoreText;
    [SerializeField]
    public int highscore;
    public GameObject highPointsIndicator;

    //Get Color of incoming key
    public Key key;
	// Use this for initialization
	void Start () {
        Score.fontSize = defaultFontSize;
        Random.InitState((int)System.DateTime.Now.Ticks);
        if(Score == null)
        {
            Debug.LogError("No Point Counter");
        }
        //HighScoreText.enabled = false;
        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }
	
    public void ScoreCounter(int increment)
    {
        Debug.Log("In Score Counter");
        totalPoints += increment;
        //If digit increased
        //If log10 incremented than the digit increased
        if(Mathf.FloorToInt(Mathf.Log10(totalPoints)) == Mathf.Log10(totalPoints))
        {
            Score.fontSize = Score.fontSize - 5;
        }
        Score.color = key.rend.color;
        Score.text = "" + totalPoints;
    }

    public void Reset()
    {
        Debug.Log("In Reset Points");
        totalPoints = 0;
        Score.color = Color.gray;
        Score.text = "" + totalPoints;
        Score.fontSize = defaultFontSize;
    }
    private void Update()
    {
        if (totalPoints > highscore)
        {
            highscore = totalPoints;
            Debug.Log("Update High Score");
            HighScoreText.text = "" + highscore;

            PlayerPrefs.SetInt("highscore", highscore);
        }

        if(key.state == Key.LevelState.GAMEOVER)
        {
            HighScoreText.enabled = true;
        }
        else
        {
           HighScoreText.enabled = false;
        }
    }

}
