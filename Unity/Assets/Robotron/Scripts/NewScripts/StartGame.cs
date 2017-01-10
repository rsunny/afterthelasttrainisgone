using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public string button = "Start";
	public string sceneToLoad = "Gameplay";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(button))
		{
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
