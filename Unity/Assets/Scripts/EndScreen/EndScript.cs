using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScript : MonoBehaviour {

	public string buttonToExit = "Fire3";
	public string sceneToLoad = "Menu";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire3"))
		{
			SceneManager.LoadScene (sceneToLoad);
		}
		
	}
}
