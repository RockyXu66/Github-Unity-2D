using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float PC_Speed;
	float Mobile_Speed = 0.4f;
	public GameObject projectile;
    public GameObject player;
	public float projectileSpeed;
	public float firingRate;
	public float health = 300f;

	private float minX, maxX, minY, maxY;


	// Use this for initialization
	void Start () {
		// Get the restrict area for play space
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 mostLeftX = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 mostRightX = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distanceToCamera));
		Vector3 mostBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 mostUpY = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distanceToCamera));

		float playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;
		float playerHeight = GetComponent<SpriteRenderer>().bounds.size.y;
		minX = mostLeftX.x + playerWidth/2.0f;
		maxX = mostRightX.x - playerWidth/2.0f;
		minY = mostBottomY.y + playerHeight/2.0f;
		maxY = mostUpY.y - playerHeight/2.0f;

		// Fire iteration
		// Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
		InvokeRepeating("Fire", 0.000001f, firingRate);
	}	

	void OnTriggerEnter2D(Collider2D collider){
//		Debug.Log(collider);

		// To check that the thing we bumped into has a Projectile componenet
		Projectile missile = collider.gameObject.GetComponent<Projectile>(); 

		// If it's true, it's collided with projectile.
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
                //Destroy(gameObject);
                gameObject.SetActive(false);
                Invoke("NewPlayer", 1f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If the platform is on PC
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.position += Vector3.up * PC_Speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			transform.position += Vector3.down * PC_Speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left*PC_Speed * Time.deltaTime;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right*PC_Speed * Time.deltaTime;
		}
		
        // Fire controlled by keyboard
		//if(Input.GetKeyDown(KeyCode.Space)){
		//	InvokeRepeating("Fire", 0.000001f, firingRate);
		//}
		//if(Input.GetKeyUp(KeyCode.Space)){
		//	CancelInvoke("Fire");
		//}

		// If the platform is on mobile
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
			transform.Translate(-touchDeltaPosition.x * Mobile_Speed * Time.deltaTime, 
								-touchDeltaPosition.y * Mobile_Speed * Time.deltaTime, 0);
        }

        // Restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(newX, newY, transform.position.z);
	}

	void Fire()
	{
		GameObject beam = Instantiate(projectile, transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}

    void NewPlayer(){
        //Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        gameObject.SetActive(true);
        // Reset health and position of the player
        health = 300f;
        gameObject.transform.position = new Vector3(0, -4, 0);
    }

}
