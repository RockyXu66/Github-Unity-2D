using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {

//	private Button printBtn;

	private GameObject autoPlayBtn; 
	private bool autoPlay = false;
	private Ball ball;

	float Mobile_Speed = 0.4f;
	
	// Use this for initialization
	void Start () {
		autoPlayBtn = GameObject.FindWithTag("AutoPlayBtn");	
		ball = GameObject.FindObjectOfType<Ball>();
//		printBtn = (GameObject.FindWithTag("QuitBtn")).GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckAutoPlay ();

		if (autoPlay) {
			AutoPlay ();
		} else {
			// Controlled by user
			if (Application.platform.ToString ().Equals ("WebGLPlayer")) {
				MoveWithFinger ();
			} else {
				MoveWithMouse ();
			}
		}

	}

	void MoveWithFinger(){
//		int nbTouches = Input.touchCount;
//		Transform transform = this.transform;
//		Vector3 paddlePos = transform.position;
//		if (nbTouches > 0) {			
//			Touch touch = Input.GetTouch (0);
//
//			float newPosX = transform.position.x;
//			if (touch.position.x / Screen.width <= 0.5f) {
//				newPosX -= 0.15f;
//			} else {
//				newPosX += 0.15f;
//			}
//			paddlePos.x = Mathf.Clamp(newPosX, 0+0.5f, 6.8f-0.5f);
//			transform.position = paddlePos;
//		}

		Vector3 paddlePos = transform.position;
		// If the platform is on mobile
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
			transform.Translate(-touchDeltaPosition.x * Mobile_Speed * Time.deltaTime, 0, 0);

			paddlePos.x = Mathf.Clamp(transform.position.x, 0+0.5f, 6.8f-0.5f);
			transform.position = paddlePos;
        }
	}

	void MoveWithMouse (){
		Transform transform = this.transform;
		Vector3 paddlePos = transform.position;
		float mousePosX = Input.mousePosition.x/Screen.width * 6.8f;
		paddlePos.x = Mathf.Clamp(mousePosX, 0+0.5f, 6.8f-0.5f);
		transform.position = paddlePos;
	}

	void AutoPlay (){
		Transform transform = this.transform;
		Vector3 paddlePos = transform.position;
		float ballX = ball.transform.position.x;
		paddlePos.x = Mathf.Clamp(ballX, 0+0.5f, 6.8f-0.5f);
		transform.position = paddlePos;
	}

	void CheckAutoPlay ()
	{
		(autoPlayBtn.GetComponent<Button> ()).onClick.AddListener (SetAutoPlay);

		// Show autoPlayBtn text
		if (autoPlay) {
			autoPlayBtn.GetComponentInChildren<Text> ().text = "AutoPlay ON";
		} else {
			autoPlayBtn.GetComponentInChildren<Text>().text = "AutoPlay OFF";
		}
	}

	void SetAutoPlay(){
		autoPlay = !autoPlay;
	}
}
