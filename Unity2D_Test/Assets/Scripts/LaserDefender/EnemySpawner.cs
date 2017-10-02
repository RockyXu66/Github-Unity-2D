using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject EnemyPrefab;
	public float speed = 2;

	private float width = 3.5f;
	private float height = 3.5f;
	private bool moveRight = true;
	private float xMax;
	private float xMin;
    private float spawnDelay = 0.5f;

	// Use this for initialization
	void Start ()
	{
        SpawnAll();

		// Get the edge position by calculating camera position and object position
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xMin = leftBoundary.x + width/2;
		xMax = rightBoundary.x - width/2;
	}

	// Update is called once per frame
	void Update()
	{
		// Move a specific direction of whole enemies
		if (moveRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// Limit the position by boundary calcuated in Start function
		if (transform.position.x < xMin)
		{
			moveRight = true;
		}
		else if (transform.position.x > xMax)
		{
			moveRight = false;
		}

		// Check if the enemy is hit by projectile
		if (AllMembersDead()){
            SpawnOneByOne();
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
    private bool AllMembersDead(){
        foreach (Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount > 0){
                return false;
            }
        }
        return true;
    }

    private void SpawnAll(){
		foreach (Transform child in transform){
			GameObject enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			// Set instantiated gameobject as a child
			enemy.transform.parent = child;
		}
    }

    private void SpawnOneByOne(){
        Transform freePosition = NextFreePosition();
        if (freePosition){
            GameObject enemy = Instantiate(EnemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
            Invoke("SpawnOneByOne", spawnDelay);
        }
    }

    private Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0)
			{
                return childPositionGameObject;
			}
		}
        return null;
    }
}
