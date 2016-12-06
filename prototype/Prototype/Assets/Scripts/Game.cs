using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0) && !DialogScript.showingDialog)
		{ 
			DialogScript.PopUpDialog("title", "I love this dialog stuff, that's not true", DialogScript.DialogType.YesNoDialog, TurnGreen, TurnRed);
		}
		
	}

	void TurnRed()
	{
		gameObject.GetComponent<Renderer>().material.color = Color.red;
	}
	void TurnGreen()
	{
		gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
}
