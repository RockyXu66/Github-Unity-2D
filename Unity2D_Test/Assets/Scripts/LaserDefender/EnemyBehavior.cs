using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float health = 300;
	public GameObject projectile;
	public float projectileSpeed;
	public float shotsPerSecond = 0.5f;

	void Update (){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}

	void Fire (){
		GameObject missile = Instantiate(projectile, transform.position + Vector3.down*0.5f, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = Vector3.down * projectileSpeed;
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
				Destroy(gameObject);
			}
		}
	}

}
