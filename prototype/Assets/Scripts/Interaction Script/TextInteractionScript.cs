using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextInteractionScript : MonoBehaviour {



	private int curState = 0;

	private int thoughtIter = 0;
	private int dialogIter = 0;

	private bool error;

	public bool isADoor = false;
	private bool workingDoor = false;
	public bool alwaysWorkingDoor = false;
	public int doorWorkingFromState = 100;
	public string sceneToLoad;

	[System.Serializable]
	public struct thoughtStruct
	{
		public int state;
		public string[] arrayOfThoughts;
		public bool changeTheState;

	};
	public thoughtStruct[] thoughtByState;
	private string[] curArrayOfThoughts = new string[0];
	private bool changeTheStateThoughts;






	[System.Serializable]
	public struct dialogStruct
	{
		public int state;
		public Line[] dialog;
		public bool changeTheState;

	};
	public dialogStruct[] dialogByState;
	private Line[] curDialog = new Line[0];

	[System.Serializable]
	public struct Line
	{
		public string[] Speakers;
		public string[] Speech;
	};
	private bool changeTheStateDialog;
	private int speechIter = 0;
	private bool moreSpeech = false;


	public bool isThereTimedThought;
	public int time = 1;
	public string[] arrayOfTimedThoughts;
	private int timedThoughtIter = 0;


	void Start()
	{

	}

	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{	
			dialogIter = 0;
			thoughtIter = 0;
			curState = PlayerInteractionManager.currentState;
			if(isADoor){
				if(alwaysWorkingDoor||curState >= doorWorkingFromState)
				{
					workingDoor = true;
				}
				else workingDoor = false;
			}
			TextScript.PopUpPressButton(workingDoor);

		}
	}
	void OnTriggerStay (Collider collideObj)
	{	


		if (collideObj.tag == "Player")
		{	
			//thoughtLength
			if(Input.GetButtonDown ("Jump"))
			{	
				Debug.Log("stato corrente e stato corrente giocatore");
				Debug.Log(curState);
				Debug.Log(PlayerInteractionManager.currentState);

				//initialization of the array of thoughts base on the current state
				for( int _i = 0; _i < thoughtByState.Length; _i++)
				{
					if (thoughtByState[_i].state == curState)
					{
						curArrayOfThoughts = thoughtByState[_i].arrayOfThoughts;
						changeTheStateThoughts = thoughtByState[_i].changeTheState;
					}
				}



				//make text appear
				if (!TextScript.showingText)
				{	

					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					//default case        yield return new WaitForSeconds(5);
					if ((curArrayOfThoughts.Length == 0 && curDialog.Length == 0)|| error )
					{
						TextScript.PopUpThought("...");
						error = false;
					}
					//start the thought at interaction thoughtIter
					StartingThought(thoughtIter);
					//update thoughtIter
					if (thoughtIter < curArrayOfThoughts.Length - 1) 
					{
						thoughtIter ++;
					}
					else
					{
						thoughtIter = 0;
					} 
				}
				//make text disappear and press Button appear
				else
				{	
					PlayerBasicMove.unstopPlayer();
					TextScript.DismissText();
					TextScript.PopUpPressButton(workingDoor);
					if(changeTheStateThoughts) PlayerInteractionManager.setCurrentState(curState+1);
				}
			}
			if(Input.GetButtonDown ("Fire3"))
			{
				
				Debug.Log("stato corrente e stato corrente giocatore");
				Debug.Log(curState);
				Debug.Log(PlayerInteractionManager.currentState);
				//initialization of the array of thoughts base on the current state
				for( int _i = 0; _i < dialogByState.Length; _i++)
				{
					if (dialogByState[_i].state == curState)
					{
						curDialog = dialogByState[_i].dialog;
						changeTheStateDialog = dialogByState[_i].changeTheState;

					} 

				}


				//make text appear
				if(!TextScript.showingText)
				{	
					speechIter = 0;
					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					//start the thought at interaction dialogIter
					if(curDialog.Length == 0 || error)
					{
						TextScript.PopUpDialog("mc:","...");
						error = false;
					}
					else StartingDialog(dialogIter, speechIter);

					//update dialogIter
					if(!moreSpeech)
					{
	
						if (dialogIter < curDialog.Length - 1) 
						{
							dialogIter ++;
						}
						else
						{
							dialogIter = 0;

						} 

					}

				}
				else if (TextScript.showingText && moreSpeech)
				{
					//dismiss all preavious tests
					TextScript.DismissText();
					StartingDialog(dialogIter, speechIter);
					//update dialogIter
					if(!moreSpeech)
					{
						if (dialogIter < curDialog.Length - 1) 
						{
							dialogIter ++;
						}
						else
						{
							dialogIter = 0;
						} 

					}
				}
				//make text disappear and press Button appear
				else if (TextScript.showingText)
				{	
					PlayerBasicMove.unstopPlayer();
					TextScript.DismissText();
					TextScript.PopUpPressButton(workingDoor);
					if(changeTheStateDialog) PlayerInteractionManager.setCurrentState(curState+1);
				}
			}

			if(Input.GetButtonDown("Fire2")&&isADoor)
			{		
				if(workingDoor)
				{
					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					SceneManager.LoadScene (sceneToLoad);
				}
			}

		}
	}
	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//dismiss all preavious tests
			TextScript.DismissText();
			//default case		
			if (arrayOfTimedThoughts.Length == 0) return;
			//start the thought at interaction timedThoughtIter
			StartingTimedthought(timedThoughtIter);
			//update timedThoughtIter
			if (timedThoughtIter < arrayOfTimedThoughts.Length - 1) 
			{
				timedThoughtIter ++;
			}
			else timedThoughtIter = 0;
	
		}
	}


	void StartingDialog(int _di, int _si)
	{	
		int speechLength = curDialog[_di].Speakers.Length - 1;
		TextScript.PopUpDialog(curDialog[_di].Speakers[_si],curDialog[_di].Speech[_si]);

		if(_si == speechLength){
			moreSpeech = false;
		}
		else
		{	
			moreSpeech = true;
			speechIter ++;
		}
	}

	void StartingThought(int _i)
	{
		if (curArrayOfThoughts.Length > _i) {
			TextScript.PopUpThought (curArrayOfThoughts [_i]);
		} else {
			TextScript.PopUpThought ("...");
		}

	}


	void StartingTimedthought(int _i)
	{
		TextScript.PopUpTimedThought(arrayOfTimedThoughts[_i]);
		StartCoroutine(waitAndDestroy());
	
	
	}

	IEnumerator waitAndDestroy()
	{

		yield return new WaitForSeconds(time);
		TextScript.DismissText();
	}
}