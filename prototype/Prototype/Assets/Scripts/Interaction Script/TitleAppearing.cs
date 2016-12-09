using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAppearing : MonoBehaviour {



	//sprite Object not working need to fix it

	public GameObject LightObject;
	public GameObject SpriteObject;


	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			LightObject.GetComponent<Light>().enabled = true;
			SpriteObject.GetComponent<Renderer>().enabled = true;
		}
	}
	void OnTriggerStay (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{

		}
	}
	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			LightObject.GetComponent<Light>().enabled = false;
			SpriteObject.GetComponent<Renderer>().enabled = false;
		}
	}
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
