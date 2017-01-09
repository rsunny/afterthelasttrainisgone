using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEscaletorScript : MonoBehaviour {

	public GameObject player;
	public bool goingDown = true;
	public float speedH = 1;
	public float speedV = 0;

	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			PlayerBasicMove.stopPlayer();
		}
	}

	void OnTriggerStay (Collider collideObj)
	{	
		if (collideObj.tag == "Player" )
		{			
			player.transform.Translate(Vector3.right * Time.deltaTime * speedH);
			player.transform.Translate(Vector3.up * Time.deltaTime * speedV);
		}
	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			PlayerBasicMove.unstopPlayer();
		}
	}
	// Use this for initialization
	void Start () 
	{
		if(goingDown) speedV = - speedV;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
