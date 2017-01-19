using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationManager : MonoBehaviour {


	/* 
	NARRATION MANAGER: 
	-it manage the text that appear walking on a Collider
	-it allow to show a thought (timed) when the player enter the collider, or when he goes away from it
	-it can be set to been show just once or more then once
	-it allows to change  the state of the game if needed
	-it can be set to be shown from a State
	-you can set the time of the appearance
	*/



	//select if the narration is on the entrance of the trigger otherwise on exit
	public bool onEnter = false;
	
	//booleans that manage if the text is going to appear once or more time
	public bool justOnce = true;
	private bool firstTime = true;

	//boolean that define if this collider is gonna change the state of the game or not, by default not
	public bool changeTheState = false;
	
	//define the starting state from which the text is gonna be shown if it is set to 0 or 1 is gonna be always shown
	public int fromState = 0;

	//the time the text is going to be on the screen, with the default time taken from canvas manager
	public int time = CanvasManager.defaultTime;

	//the actual text that is going to appear, with the default text
	public string narration = "should I think about something now?";
	
	//the eventual image to show (by default is null)
	public Sprite imageToShow = null;

	//private int to manage the state
	private int curState = 0;

	//private function that show the image on screen 
	private void showNarration()
	{
		string _narration ;
		Sprite _image = null;
		int _time = 1;

		_narration = narration;
		_image = imageToShow;
		_time = time ;

		CanvasManager.ShowTimedTought(_narration, _image, _time );

		if(changeTheState && firstTime)
		{
			StateManager.setCurrentState(curState + 1);
		}
		firstTime = false;

	}


	
	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" && onEnter)
		{
			curState = StateManager.currentState;
			if(!justOnce)
			{
				Debug.Log(fromState);
				Debug.Log(curState);
				if((fromState <= curState /* && fromState < StateManager.maxState*/) || fromState <= 0)
				{
					showNarration();
				}
			}
			else if (justOnce)
			{
				if((fromState <= curState /* && fromState < StateManager.maxState*/) || fromState <= 0)
				{
					if(firstTime /*&& FirstTimeCheck.firstTime*/) showNarration();
				}
			}
		}
	}
	
	void OnTriggerStay (Collider collideObj)
	{

	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player" && !onEnter)
		{
			
			if(!justOnce)
			{

				if((fromState >= curState /* && fromState < StateManager.maxState*/) || fromState <= 0)
				{
					showNarration();
				}
			}
			else if (justOnce)
			{
				if((fromState >= curState /* && fromState < StateManager.maxState*/) || fromState <= 0)
				{
					if(firstTime) showNarration();
					firstTime = false;
				}
			}
	
		}
	}
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
