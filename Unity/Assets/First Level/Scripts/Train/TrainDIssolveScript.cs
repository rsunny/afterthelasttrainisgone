using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDIssolveScript : MonoBehaviour {


	public GameObject train;

	// Use this for initialization
	void Start () 
	{
		//train = GameObject.Find("Train");
	}

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.name == "Train")
		{
			Debug.Log("sono qui");
			//train.SetActive(false);
			Destroy(train);
		}
	}
	void OnTriggerStay (Collider collideObj)
	{

			Debug.Log("sono qui stay");
			//train.SetActive(false);
			Destroy(train);

	}
	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.name == "Train")
		{
			Debug.Log("sono qui exit");
			//train.SetActive(false);
			Destroy(train);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
