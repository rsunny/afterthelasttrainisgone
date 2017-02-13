using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ManageGame.setHealth(90);
        ManageGame.setSceneName("FirstLevel");
        ManageGame.setState(0);
        ManageGame.setSceneName("SecondLevel");
        ManageGame.Save();
    }

}
