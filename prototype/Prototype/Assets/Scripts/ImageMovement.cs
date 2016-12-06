using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMovement : MonoBehaviour {

	[Range(-900f,900f)]
	public float destination = 900f;
	[Range(-900f,900f)]
	public float startingPoint = -900f;
	[Range(0f,50f)]
	public float speed = 10f;


	// Use this for initialization
	void Start () {

				
		transform.localPosition = new Vector3 (startingPoint, transform.localPosition.y, 0);
		
		if (destination < startingPoint) 
		{
			speed = -speed;
		}


	}
	
	// Update is called once per frame
	void Update () {
		
		if (destination < startingPoint)
		{
			if (transform.localPosition.x > destination) Move() ;
		}
		else if (destination > startingPoint) {
	
			if (transform.localPosition.x < destination) Move() ;
		}
				

		
	}

	void Move ()
	{	

		transform.Translate(Vector3.right* speed * Time.deltaTime);

	}

}
