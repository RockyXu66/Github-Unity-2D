using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int totalScore;

	// Use this for initialization
	void Start () {
		totalScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text = "Score "+totalScore.ToString();
	}
}
