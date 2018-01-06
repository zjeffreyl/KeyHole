using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    [SerializeField]
    private Text Score;
    public int totalPoints;

    //Get Color of incoming key
    public Key key;
	// Use this for initialization
	void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        if(Score == null)
        {
            Debug.LogError("No Point Counter");
        }
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

    public void RestPoints()
    {
        Debug.Log("In Reset Points");
        totalPoints = 0;
        Score.color = Color.gray;
        Score.text = "" + totalPoints;
    }

}
