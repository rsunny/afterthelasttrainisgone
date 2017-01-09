using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitFromRobotron : MonoBehaviour {

	public string button = "e";
	public string sceneToLoad = "LostAndFound";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(button))
		{
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
