using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collideObj)
    {
        ManageGame.setHealth(90);
        ManageGame.setSceneName("FirstLevel");
        ManageGame.setState(1);
        Debug.Log("In collison");
        Debug.Log(ManageGame.getHealth());
        Debug.Log(ManageGame.getSceneName());
        Debug.Log(ManageGame.getState());
        ManageGame.setSceneName("SecondLevel");
        ManageGame.Save();
    }

}
