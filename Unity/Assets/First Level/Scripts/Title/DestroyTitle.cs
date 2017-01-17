using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTitle : MonoBehaviour {

	public GameObject titleToDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			titleToDestroy.SetActive(false);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
