using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroyObjectsAtState : MonoBehaviour {

	public int fromState = 0;
	private int curState = 0;
	private bool done = false;
	public GameObject objectToCreate = null;
	public GameObject objectToDestroy = null;

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player" && !done)
		{
			curState = StateManager.currentState;
			if(curState >= fromState)
			{
				if (objectToDestroy != null)
				{
					objectToDestroy.SetActive(false);
				}
				if (objectToCreate != null)
				{
					objectToCreate.SetActive(true);
				}
				done = true;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
