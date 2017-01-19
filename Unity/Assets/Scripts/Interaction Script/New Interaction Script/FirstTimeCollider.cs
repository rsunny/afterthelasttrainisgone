using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeCollider : MonoBehaviour {


	/*
	FirstTimeCollider:
	this collider must be placed at the first entrance of the scene, it set the boolean firstTime to false the first time you enter the scene;
	so help destroying the colliders and undesired object.

	*/

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			Debug.Log("first time before change " +  FirstTimeCheck.firstTime);
			FirstTimeCheck.firstTime = false;
			Debug.Log("first time after change " +  FirstTimeCheck.firstTime);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
