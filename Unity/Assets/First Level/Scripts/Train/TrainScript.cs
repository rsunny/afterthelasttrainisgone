using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour {


	public GameObject train;
	public float trainSpeed = 0.1f;
	public float trainAcceleration = 0.1f;
	private bool trainStart = false;

	// Use this for initialization
	void Start () 
	{
		train = GameObject.Find("Train");
	}
	
	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			trainStart = true;
		}
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(trainStart == true)
		{
			trainSpeed = trainSpeed + (Time.deltaTime * trainAcceleration);
			train.transform.Translate( Vector3.left * trainSpeed);
		}
	}
}
