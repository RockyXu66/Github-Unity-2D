using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCollider : MonoBehaviour {

	private GameObject playAgainBtn;

//	private LevelManager levelManager;

	void Start(){

		playAgainBtn = GameObject.FindWithTag("PlayAgainBtn");
		playAgainBtn.SetActive(false);
//		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D (Collider2D trigger){

		playAgainBtn.SetActive(true);
	}
}
