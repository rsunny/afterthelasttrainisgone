using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InteractionManager : MonoBehaviour {
	
	//default thought e dialog
	private string defaultThought = "I don't know now what to think";
	private string defaultWhoIsSpeaking = "Main Character:";
	private string defaultDialog = " I don't know what to say";
	//button interaction
	public string ButtonString = "press E to interact";
	//button to press
	private string button = "e";



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
		public bool changeTheState; // = false;
		
		//boolean that say if it is the first time that you interact or it has something new to say
		public bool isDialogue;
		public ThoughtStruct[] Thoughts;
		public DialogStruct[] Dialogs;

		//tell if it is a door or not, and if can be open that state...
		public bool openableDoor;
		public string sceneToLoad;

		//still need to decide if use them or not
		public bool needAnObject; // = false;
		public bool giveAnObject; // = false;
		
		//still to decide if use them or not
		public bool interactable;// = false;
		public interactionDelegate interaction;
	}

	public InteractionStruct[] Interactions = null;



	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//show the botton
			//initialized the Variables
			

			//dismiss previous text form narration or other colliders
			CanvasManager.DismissAll();
			
			//show the botton  NOTA: bisogna ancora impementare un modo per cambiare colore se non c'è più nulla da dire!!
			CanvasManager.ShowButton(ButtonString, firstTime);


		}

	}

	void OnTriggerStay (Collider collideObj)
	{
		if (collideObj.tag == "Player" && Input.GetKeyDown (button))
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
				//show the botton  
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

		//Iteration     ** not clean code **
		iterator ++;
		//here there is the iteration that set the first time to false
		if(iterator >= _curIterationLength -1)
		{
			firstTime = false;
		}
		//this is the one that set the iteration
		if(iterator >= _curIterationLength)
		{
			iterator = 0;
			//state changing
			isTheStateChanging(_curStruct.changeTheState);
			//check if is a door that can be open in this state and open it
			if(_curStruct.openableDoor)
			{
				if(_curStruct.sceneToLoad == null) return;
				CanvasManager.DismissAll();
				SceneManager.LoadScene (_curStruct.sceneToLoad);
			}
		}

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
		
		//default case if _curDialog is empty
		if(_curDialogs.Length == 0)
		{
			_curWhoIsSpeaking = defaultWhoIsSpeaking;
			_curDialog = defaultDialog;
		}
		else
		{
			_curWhoIsSpeaking = _curDialogs[iterator].WhoIsSpeaking;
			_curDialog = _curDialogs[iterator].Dialog;
		}


		//default case if something inside is empty
		if(_curWhoIsSpeaking == null) _curWhoIsSpeaking = defaultWhoIsSpeaking;
		if(_curDialog == null) _curDialog = defaultDialog;

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

		if(transform.parent.GetComponent<SpriteRenderer>() != null)
		{
			CurrentSprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
