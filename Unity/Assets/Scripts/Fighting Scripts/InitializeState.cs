using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeState : MonoBehaviour {

	private bool m_alreadyActivated =false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" && !m_alreadyActivated)
		{
			StateManager.InitializeState();
			m_alreadyActivated = true;
		}
	}
}
