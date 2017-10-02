using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public int maxHits;
	private int timesHits;
	private Color newColor;
	private GameObject score;
	private LevelManager levelManager;

	public GameObject smoke;

	// If it's static, there're only one integer brickCount in the game.
	public static int brickCount = 0;
	
	// Use this for initialization
	void Start () {
		timesHits = 0;
		newColor = GetComponent<SpriteRenderer>().color;
		score = GameObject.FindWithTag("ScoreTxt");
		levelManager = GameObject.FindObjectOfType<LevelManager>();

		brickCount ++;
	}
	
	// Update is called once per frame
	void Update () {
		if(maxHits == timesHits){
			brickCount --;
			Destroy(gameObject);

			PuffSmoke();

			// Check if all brickes are destroyed
			levelManager.AllBricksDestroyed();
		}

	}

	void PuffSmoke(){
		// Instantiate the smoke
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		Color spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
		smokePuff.GetComponent<ParticleSystem>().startColor = spriteColor;
	}

	void OnCollisionExit2D(Collision2D collision){
		timesHits ++;
		// Add scores
		score.GetComponent<Score>().totalScore ++;

		// Change brick's color
		newColor.a -= 0.2f;
		GetComponent<SpriteRenderer>().color = new Color(newColor.r, newColor.g, newColor.b, newColor.a );
	}


}
