using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	private Paddle paddle;

	public int TotalScore;

	private GameObject startBtn;

	private bool hasStarted = false;

	private Vector3 paddleToBallVector;

	// Use this for initialization
	void Start () {
		// Get the paddle object
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;	

		startBtn = GameObject.FindWithTag("StartBtn");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Application.platform.ToString ().Equals ("WebGLPlayer")) {
			(startBtn.GetComponent<Button> ()).onClick.AddListener (LaunchBall);
		} else {
			startBtn.SetActive(false);
		}


		if (!hasStarted) {
			// Lock the ball relative to the paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;

			// Wait for a mouse press to launch
			if(Input.GetMouseButtonDown(0)){
				hasStarted = true;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);	
			}
		}
	}

	void LaunchBall (){
		hasStarted = true;
		startBtn.SetActive(false);
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);	
	}

	void OnCollisionEnter2D(Collision2D collision){
		Vector2 tweak = new Vector2(Random.Range(0f, 0.5f), Random.Range(0f, 0.5f));
		if(hasStarted){
			(this.GetComponent<Rigidbody2D>()).velocity += tweak;
		}
	}
}
