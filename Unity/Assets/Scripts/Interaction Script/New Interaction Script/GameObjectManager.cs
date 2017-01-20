using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour {




	/*
	GameObjectManager:
	this class define for each scene if an object exist in that scene or not, based on the state
	this class manage also the narration, that have first time flag, that the second time you go inside the scene will be deactivated 
	*/
	[System.Serializable]
	public struct ObjectStruct
	{
		public GameObject selectedObject;
		public int fromState;
	}
	
	public  ObjectStruct[] objectToCreate;
	public  ObjectStruct[] objectToDestroy;
	public GameObject [] oneTimeColliders;

	void Awake()
	{

		for( int _i = 0; _i < objectToCreate.Length; _i++)
		{
			Debug.Log ("create object from state: " + objectToCreate[_i].fromState + " currentState : " + StateManager.currentState );
			if(objectToCreate[_i].fromState <= StateManager.currentState)
			{
				objectToCreate[_i].selectedObject.SetActive(true);
			}
			//else objectToCreate[_i].selectedObject.SetActive(false);
		}

		for( int _i = 0; _i < objectToDestroy.Length; _i++)
		{
			if(objectToDestroy[_i].fromState <= StateManager.currentState)
			{
				objectToDestroy[_i].selectedObject.SetActive(false);
			}
			//else objectToDestroy[_i].selectedObject.SetActive(true);
		}

		if(!FirstTimeCheck.firstTime)
		{
			for( int _i = 0; _i < oneTimeColliders.Length; _i++)
			{
				oneTimeColliders[_i].SetActive(false);
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
