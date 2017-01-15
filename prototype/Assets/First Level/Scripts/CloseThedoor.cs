using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseThedoor : MonoBehaviour {

	public GameObject doorOpen = null ;
	public GameObject doorClosed = null;
	public GameObject doorClosed2 = null;
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			if (doorOpen != null) doorOpen.SetActive(false);
			if (doorClosed != null) doorClosed.SetActive(true);
			if (doorClosed2 != null) doorClosed2.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
