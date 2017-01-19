using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCGuyScript : MonoBehaviour {



	public GameObject StrangeGuy;
	public GameObject startMarker;
	public GameObject endMarker;
	public bool isOut = false;
	public bool move = false;
	public float StartTime;
	public float JourneyLength;
	public float speed = 0.01f;





	



	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			move = true;
		}
	}

	// Use this for initialization
	void Start () 
	{
		StrangeGuy = GameObject.Find("StrangeGuy");
		startMarker = GameObject.Find("startMarker");
		endMarker = GameObject.Find("endMarker");
		StartTime = Time.time;
		JourneyLength = Vector3.Distance (startMarker.transform.position, endMarker.transform.position);
		Debug.Log("distance " + JourneyLength + " endMarker " + endMarker.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(move);
		if(move){
			Debug.Log("sono qui");
			float distCovered = (Time.time - StartTime) * speed;
			float fracJourney = distCovered / JourneyLength;
			StrangeGuy.transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, 0.5f);	
			move = false;	
			isOut = true;
		}
	}
}
