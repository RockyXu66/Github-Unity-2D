using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	private enum States { School, Home, Theater, ShoppingMall };

	private States myState;
	
	public Text text;
	public Button pressMeBtn;
	public Button originalTextBtn;

	// Use this for initialization
	void Start () {
//		myState = States.School;

		pressMeBtn.onClick.AddListener(PressMeOnClick);
		originalTextBtn.onClick.AddListener(OriginalTextOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			text.text = "Space key is pressed";
		}

	}

	void PressMeOnClick(){
		text.text = "Button is pressed";
    }

	void OriginalTextOnClick(){
		text.text = "This is a text test for UI.\n"+
					"The top image is added as an UI image.";
    }
}
