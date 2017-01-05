using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseThedoor : MonoBehaviour {

	public GameObject doorOpen = null ;
	public GameObject doorClosed = null ;
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" && doorOpen != null && doorClosed != null)
		{
			doorOpen.SetActive(false);
			doorClosed.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
