using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAppearing : MonoBehaviour {


	public GameObject Light;


	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			Debug.Log("enter");
			Light.SetActive(true);
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
			Debug.Log("escher");
			Light.SetActive(false);	
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
