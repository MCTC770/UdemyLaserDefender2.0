using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text scoreUi;

	void Start()
    {
        scoreUi = GetComponent<Text>();
        Reset();
    }
	
    public void Score(int points)
    {
        score = score + points;
        Debug.Log("Total points: " + score);
        scoreUi.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }

}
