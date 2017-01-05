using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMovement : MonoBehaviour {

	[Range(0f,10f)]
	public float rotationSpeed = 1;
	private bool right = true;
	private float angle = 0;

	void Start () 
	{
		rotationSpeed = rotationSpeed * (-10);
	}
	
	void Update () 
	{
	}

	void FixedUpdate () 
	{
		angle = transform.eulerAngles.x ;
		Debug.Log (angle); 
		Debug.Log (right);
		if (right == false)
		{
			transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
			if ( angle >= 30 && angle <= 330) right = true;
			else {right = false;}
		}
		else if(right = true)
		{
			transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
			if ( angle >= 30 && angle <= 330) right = false;
		}
		//Debug.Log(angle);
	}
}
