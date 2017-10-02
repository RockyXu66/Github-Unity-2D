using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Destroy the projectile if collided
	void OnTriggerEnter2D(Collider2D col){
		Destroy(col.gameObject);
	}
}
