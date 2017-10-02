using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;


	void Awake (){

		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			// Keep sounds all the time without destroy it 
			GameObject.DontDestroyOnLoad(gameObject);	// this gameObject is an auido here
		}
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
