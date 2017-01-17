﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitFromRobotron : MonoBehaviour {

	public string button = "Exit";
	public string sceneToLoad = "LostAndFound";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(button))
		{
			var objects = GameObject.FindGameObjectsWithTag ("Robotron");
			foreach (GameObject obj in objects){
				Destroy (obj);
			}
			
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
