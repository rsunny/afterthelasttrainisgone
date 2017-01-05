using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOtherCollider : MonoBehaviour {



	public GameObject otherCollider = null;

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" && otherCollider != null)
		{
			otherCollider.SetActive(true);
		}
	}
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
