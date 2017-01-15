using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour {
	public bool isADoor = false;



	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{

		}
	}
	void OnTriggerStay (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			if(Input.GetKeyDown ("w"))
			{	
				SceneManager.LoadScene ("Level1");
			}
		}
	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
