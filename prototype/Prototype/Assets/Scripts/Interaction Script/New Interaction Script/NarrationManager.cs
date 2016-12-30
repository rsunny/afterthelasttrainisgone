using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationManager : MonoBehaviour {

	//select if the narration is on the entrance of the trigger otherwise on exit
	public bool onEnter = false;
	
	public bool justOnce = true;
	private bool firstTime = true;

	public bool changeTheState = false;
	
	public int fromState = 0;

	public int time = CanvasManager.defaultTime;
	public string narration = "should I think about something now?";
	
	public Sprite imageToShow = null;


	private int curState = 0;

	
	private void showNarration(int _fromState = 0)
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
					if(firstTime) showNarration();
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
