using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour {
	
	//default thought e dialog
	private string defaultThought = "I don't know now what to think";
	private string defaultWhoIsSpeaking = "Main Character:";
	private string defaultDialog = " I don't know what to say";
	//button interaction
	public string ButtonString = "press E to interact";



	//current state
	private int curState = 1;
	//previous state
	private int previousState;

	//boolean to identify first time you interacct with an object and if it has something new to say
	public bool firstTime = true;

	//iterator of texts inside a state
	private int iterator = 0;




	[System.Serializable]
	public struct ThoughtStruct
	{
		public string Thought;
		public Image ThoughtImage;
	}

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
		public bool interactable;// = false;
		public interactionDelegate interaction;
		public bool needAnObject; // = false;
		public bool giveAnObject; // = false;
		public bool changeTheState; // = false;
	}

	public InteractionStruct[] Interactions = null;



	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//show the botton
			//initialized the Variables
			
			//in this way any time you entry the collider the conversations starts again from the begininng
			//iterator = 0;
			
			//show the botton  NOTA: bisogna ancora impementare un modo per cambiare colore se non c'è più nulla da dire!!
			CanvasManager.ShowButton(ButtonString, firstTime);


		}

	}

	void OnTriggerStay (Collider collideObj)
	{
		if (collideObj.tag == "Player" && Input.GetKeyDown ("e"))
		{


			//show the text and dismiss the button
			if(CanvasManager.showingText == false)
			{
				//dismiss button
				CanvasManager.DismissButton();
				
				
				//initialize the state
				curState = StateManager.currentState;

				if(Interactions.Length == 0)
				{
					CanvasManager.ShowThought( defaultThought, null );
					firstTime = false;
				} 
				else 
				{
					Interact(curState);
				}
				//show either a dialog or a thought, base on the state
			}
			
				
			//dismiss the text and show the button again
			else if (CanvasManager.showingText == true)
			{
				CanvasManager.DismissAll();
				//show the botton  NOTA: bisogna ancora impementare un modo per cambiare colore se non c'è più nulla da dire!!
				CanvasManager.ShowButton( ButtonString, firstTime);
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
			if(previousState != _curState)
			{
				firstTime = true;
				previousState = _curState;
			}
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

		//need to check the maximum size of the iterator
		iterator ++;
		if(iterator >= _curIterationLength -1)
		{
			iterator = 0;
			firstTime = false;
		}
		
	}

	private void displayThought(ThoughtStruct[] _curThoughts )
	{

		//if(iterator >= _curThoughts.Length)iterator = 0;
		string _curThought = _curThoughts[iterator].Thought;
		Image _curImage = _curThoughts[iterator].ThoughtImage;


		//default case if something is empty
		if(_curThought == null) _curThought = "...";
		if(!_curImage) _curImage = null;

		CanvasManager.ShowThought( _curThought,  _curImage );

	}

	private void displayDialog(DialogStruct[] _curDialogs )
	{
		string _curWhoIsSpeaking = "";
		string _curDialog = "...";
		
		//if(iterator >= _curDialogs.Length) iterator = 0;
		if(_curDialogs.Length == 0)
		{
			_curWhoIsSpeaking = defaultWhoIsSpeaking;
			_curDialog = defaultDialog;

		}
		_curWhoIsSpeaking = _curDialogs[iterator].WhoIsSpeaking;
		_curDialog = _curDialogs[iterator].Dialog;


		//default case if something is empty
		if(_curWhoIsSpeaking == null) _curWhoIsSpeaking = "";
		if(_curDialog == null) _curDialog = "...";

		CanvasManager.ShowDialog( _curWhoIsSpeaking,  _curDialog );
	}


	private void isTheStateChanging (bool _changeTheState)
	{
		if(_changeTheState)
		{
			StateManager.updateState();
		}
		else return;
	}



	// Use this for initialization
	void Start () 
	{
		previousState = StateManager.currentState;
		firstTime = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
