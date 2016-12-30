using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorManager : MonoBehaviour {



	public string SceneToLoad;
	public bool isOpen = true;
	public int openFromState = 0;
	private int currentState;
	public int openUntilState = 100; 
	public string thoughtIfClosed = "It is closed";
	public string ButtonString = "press E to open";
	public string button = "e";
	
	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{	
			currentState = StateManager.currentState;
			//dismiss previous text form narration or other colliders
			CanvasManager.DismissAll();
			
			//show the botton 
			CanvasManager.ShowButton(ButtonString, isOpen);
		}
	}
	
	void OnTriggerStay (Collider collideObj)
	{	
		if (collideObj.tag == "Player" && Input.GetKeyDown (button))
		{			
			//show the text and dismiss the button
			if(CanvasManager.showingText == false)
			{
				CanvasManager.DismissButton();

				if( isOpen || (currentState >= openFromState && currentState <= openUntilState))
				{
						SceneManager.LoadScene (SceneToLoad);
				}
				else
				{
					CanvasManager.ShowThought(thoughtIfClosed);
				}
			}
			else if(CanvasManager.showingText == true)
			{
				CanvasManager.DismissAll();
				//show the botton  
				CanvasManager.ShowButton(ButtonString, isOpen);
			}
		}
	}	

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//dismiss all text
			CanvasManager.DismissAll();
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
