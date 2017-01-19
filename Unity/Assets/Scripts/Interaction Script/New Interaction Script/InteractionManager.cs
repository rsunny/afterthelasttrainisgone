﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class InteractionManager : MonoBehaviour {

	private GameObject playerObject;
	//SOUND
	public AudioClip m_doorSound;
	public AudioSource m_audioSource;

	//default thought e dialog
	private string defaultThought = "I don't know now what to think";
	private string defaultWhoIsSpeaking = "Arthur Treyu:";
	private string defaultDialog = " I don't know what to say";
	//button interaction
	public string ButtonString = "press E to interact";
	//button to press
	private string button = "Fire3";
	private bool buttonValue = false;
	//player in an interaction trigger
	private bool trig;


	//current state
	private int curState = 1;
	//previous state
	private int previousState = -1;

	//boolean to identify first time you interacct with an object and if it has something new to say
	private bool firstTime = true;

	//iterator of texts inside a state
	private int iterator = 0;

	/*OLD*/
	//iteator for a single dialog CAN FIND THIS IN INTERACTION MANAGER OLD
	private int dialogIterator = 0;
	private bool keepTalking = false;




	[System.Serializable]
	public struct ThoughtStruct
	{
		//the image is shown only if the showImage flag is set to true, in this case, or you can chose what image to show or he is going to show the sprite
		public string Thought;
		public bool showImage;
		public Sprite ThoughtImage;
	}
	private Sprite CurrentSprite = null;


	[System.Serializable]
	public struct DialogStruct
	{
		public string WhoIsSpeaking;
		public string Dialog;
	}


	[System.Serializable]
	public delegate void interactionDelegate();

	[System.Serializable]
	public struct InteractionStruct
	{

		//boolean that say if it is the first time that you interact or it has something new to say
		public bool isDialogue;
		public ThoughtStruct[] Thoughts;

		public DialogStruct[] Dialogs;

		public bool changeTheState; // = false;
		public string changingStateSentence;
		public Sprite stateImage;
		public int timeToDestroyStateCanvas;
		public bool stateChanged;
		
		
		//tell if it is a door or not, and if can be open that state...
		public bool openableDoor;
		public string sceneToLoad;
		public string changingSceneSentence;

		//still need to decide if use them or not
		//public bool needAnObject; // = false;
		//public bool giveAnObject; // = false;

		//still to decide if use them or not
		//public bool interactable;// = false;
		//public interactionDelegate interaction;
	}

	public InteractionStruct[] Interactions = null;

	/*OLD*/
	/*[System.Serializable]
	public class DialogStructs
	{
		public DialogStruct[] singleDialog;
	}
	*/


	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//show the botton
			//initialized the Variables

			//dismiss previous text form narration or other colliders
			CanvasManager.DismissAll();

			/*OLD*/
			//keepTalking = false;

			//This is the check if the state change on trigger enter, and to update the button
			if (previousState != StateManager.currentState && Interactions.Length > StateManager.currentState)
			{

				firstTime = true;
				previousState = StateManager.currentState;			
			}


			//show the botton  NOTA: bisogna ancora impementare un modo per cambiare colore se non c'è più nulla da dire!!
			CanvasManager.ShowButton(ButtonString, firstTime);

			trig = true;
		}

	}

	/*void OnTriggerStay (Collider collideObj)*/
	void Interaction()
	{
		if (trig) {
			//This is the check if the state change while you stay in the same trigger, and to update the button
			if (/*collideObj.tag == "Player" &&*/ previousState != StateManager.currentState && Interactions.Length > StateManager.currentState) {
				firstTime = true;
				previousState = StateManager.currentState;			
			}

			Debug.Log (StateManager.currentState);

			//when you press the interaction button
			if (/*collideObj.tag == "Player" &&*/ buttonValue) {
				buttonValue = false;
				//show the text and dismiss the button
				if (CanvasManager.showingText == false) {
					//dismiss button
					CanvasManager.DismissButton ();


					//initialize the state
					curState = StateManager.currentState;



					if (Interactions.Length == 0) {
						CanvasManager.ShowThought (defaultThought, null);
						firstTime = false;
					} else {
						Interact (curState);
					}
					//show either a dialog or a thought, base on the state
				}

			/*OLD*/

			else if (CanvasManager.showingText == true && keepTalking) {
					CanvasManager.DismissAll ();
					Interact (curState);
				}/*
			*/

			//dismiss the text and show the button again
			else if (CanvasManager.showingText == true) {
					CanvasManager.DismissAll ();
					//show the botton  
					CanvasManager.ShowButton (ButtonString, firstTime);
				}


			}
		}

	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//dismiss all text
			CanvasManager.DismissAll();


			keepTalking = false;
			dialogIterator = 0;


			//This set dialog iteretor to 0 if you go out from the collider,
			//that's because make no sense to keep dialoging like nothing happend if sameone goes around
			if(Interactions.Length != 0 && Interactions.Length <= StateManager.currentState && Interactions[0].isDialogue )
			{
				iterator = 0;
			}
			else if(Interactions.Length != 0 && Interactions.Length > StateManager.currentState && Interactions[StateManager.currentState].isDialogue ) 
			{
				iterator = 0;
			}
			trig = false;

		}

	}



	private void Interact(int _curState)
	{
		bool _isDialog = false;
		InteractionStruct _curStruct;
		int _curIterationLength;

		//normal case: if it is defined, is the normal thought/dialog associated to the current state
		if(Interactions.Length > _curState)
		{
			_curStruct = Interactions[_curState];
			_isDialog = _curStruct.isDialogue;

		}

		//case: the state 0 (so Interactions[0]) is always the default case for an object
		else
		{
			_curStruct = Interactions[0];
			_isDialog = _curStruct.isDialogue;
		}

		if(_isDialog)
		{
			_curIterationLength = _curStruct.Dialogs.Length;
			displayDialog(_curStruct.Dialogs);
		} 

		else
		{
			_curIterationLength = _curStruct.Thoughts.Length;
			displayThought(_curStruct.Thoughts);
		}

		//here there is the iteration that set the first time to false
		if(iterator >= _curIterationLength -1)
		{
			firstTime = false;
		}
		//Iteration     ** not clean code **
		iterator ++;

		//this is the one that set the iteration
		if(iterator >= _curIterationLength)
		{
			iterator = 0;
			//state changing
			isTheStateChanging(_curStruct.changeTheState&&(!(_curStruct.stateChanged)));
			//check if is a door that can be open in this state and open it
			if(_curStruct.openableDoor)
			{
				if(_curStruct.sceneToLoad == null) return;

				string sentenceToShow = "opening door...";
				if(String.IsNullOrEmpty(_curStruct.changingSceneSentence)) sentenceToShow = _curStruct.changingSceneSentence;
				CanvasManager.ShowChangeState(sentenceToShow, null ,3 );
				playerObject.GetComponent<PlayerManager> ().DisableAction (true);

				StartCoroutine (OpenDoor (_curStruct.sceneToLoad));

			}
		}

	}

	private IEnumerator OpenDoor (string scene){
		if (m_audioSource != null) {
			m_audioSource.PlayOneShot (m_doorSound);
		}
		yield return new WaitForSeconds (2);
		CanvasManager.DismissAll();
		SceneManager.LoadScene (scene);
	}


	private void displayThought(ThoughtStruct[] _curThoughts )
	{

		//if(iterator >= _curThoughts.Length)iterator = 0;
		string _curThought = defaultThought;
		Sprite _curImage = null;

		if(_curThoughts.Length == 0)
		{
			_curThought = defaultThought;
			_curImage = null;
		}
		else 
		{
			_curThought = _curThoughts[iterator].Thought;
			if (_curThoughts[iterator].showImage)
			{	
				if( _curThoughts[iterator].ThoughtImage == null)
				{
					_curImage = CurrentSprite;
				}
				else 
				{
					_curImage = _curThoughts[iterator].ThoughtImage;
				}
			}
		}

		//default case if something is emptyCurrentSprite
		if(_curThought == null) _curThought = defaultThought;
		if(!_curImage) _curImage = null;

		CanvasManager.ShowThought( _curThought,  _curImage );

	}

	private void displayDialog(DialogStruct[] _curDialogs )
	{
		string _curWhoIsSpeaking = defaultWhoIsSpeaking;
		string _curDialog = defaultDialog;
		int dialogLength = _curDialogs.Length;

		//default case if _curDialog is empty
		if(dialogLength == 0 )
		{
			_curWhoIsSpeaking = defaultWhoIsSpeaking;
			_curDialog = defaultDialog;
		}
		else
		{
			/*OLD*/
			keepTalking = true;
			_curWhoIsSpeaking = _curDialogs[iterator].WhoIsSpeaking; //.singleDialog[dialogIterator].WhoIsSpeaking;
			_curDialog = _curDialogs[iterator].Dialog;  //.singleDialog[dialogIterator].Dialog;
			/*OLD*/
			dialogIterator ++;

			/*OLD*/

			if(dialogLength <= dialogIterator)
			{	

				dialogIterator = 0;	
				keepTalking = false;
			}
		}


		//default case if something inside is empty
		if(_curWhoIsSpeaking == null) _curWhoIsSpeaking = defaultWhoIsSpeaking;
		if(_curDialog == null) _curDialog = defaultDialog;

		CanvasManager.ShowDialog( _curWhoIsSpeaking,  _curDialog );
	}

	private void displayStateChanging()
	{
		string _sentence;
		int _time = 2;
		Sprite _stateImage = null;

		InteractionStruct _tempStruct = Interactions[StateManager.currentState];
		if(String.IsNullOrEmpty(_tempStruct.changingStateSentence) ) return;
		_sentence = _tempStruct.changingStateSentence;
		_time = _tempStruct.timeToDestroyStateCanvas;
		_stateImage = _tempStruct.stateImage;
		
		CanvasManager.ShowChangeState(_sentence,_stateImage,_time );

	}
	private void isTheStateChanging (bool _changeTheState)
	{
		if(_changeTheState)
		{
			displayStateChanging();
			StateManager.updateState();
		}
		else return;
	}



	// Use this for initialization
	void Start () 
	{
		previousState = StateManager.currentState;
		firstTime = true;

		/*OLD*/
		//keepTalking = false;

		playerObject = GameObject.Find("Player"); 

		if(transform.parent.GetComponent<SpriteRenderer>() != null)
		{
			CurrentSprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
		}

	}

	// Update is called once per frame
	void Update () {
		buttonValue = Input.GetButtonDown (button);
		Interaction ();
	}
}
