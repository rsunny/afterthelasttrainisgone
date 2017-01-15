using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour {

	[Range(0f,10f)]
	public float rotationSpeed = 7;

	void Start () 
	{
		rotationSpeed = rotationSpeed * (-10);
	}
	
	void Update () 
	{
	}

	void FixedUpdate () 
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
	}
}
