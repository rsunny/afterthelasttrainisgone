using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialStateCollider : MonoBehaviour {
	/*
	SetInitialStateCollider:
	this collider must be placed at the first entrance of the scene, it set the state, if desired, to 0 the first time you enter the scene;
	NOTE: IT MUST BE PLACED BEFORE FIRST TIME COLLIDER

	*/

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
				Debug.Log("state before changing " +  StateManager.currentState);
				StateManager.InitializeState();
				Debug.Log("state after changing " +  StateManager.currentState);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
