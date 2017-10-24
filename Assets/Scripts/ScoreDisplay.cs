using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start () {
        Text finalScore = GetComponent<Text>();
        finalScore.text = ScoreKeeper.score.ToString();
    }
	
	void Update () {
		
	}
}
