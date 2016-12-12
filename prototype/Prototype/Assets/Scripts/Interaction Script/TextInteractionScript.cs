using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInteractionScript : MonoBehaviour {

	public int curState = 0;

	private int thoughtIter = 0;
	private int dialogIter = 0;
	
	[System.Serializable]
	public struct thoughtStruct
	{
		public int state;
		public string[] arrayOfThoughts;

	};
	public thoughtStruct[] thoughtByState;
	private string[] curArrayOfThoughts;






	[System.Serializable]
	public struct dialogStruct
	{
		public int state;
		public Line[] dialog;
	};
	public dialogStruct[] dialogByState;
	private Line[] curDialog;

	[System.Serializable]
	public struct Line
	{
		public string[] Speakers;
		public string[] Speech;
	};
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
			TextScript.PopUpPressButton();
		}
	}
	void OnTriggerStay (Collider collideObj)
	{	


		if (collideObj.tag == "Player")
		{	
			//thoughtLength
			if(Input.GetKeyDown ("e"))
			{	

				//initialization of the array of thoughts base on the current state
				for( int _i = 0; _i < thoughtByState.Length; _i++)
				{
					if (thoughtByState[_i].state == curState) curArrayOfThoughts = thoughtByState[_i].arrayOfThoughts;
				}


				//make text appear
				if (!TextScript.showingText)
				{	
					

					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					//default case        yield return new WaitForSeconds(5);
					if (curArrayOfThoughts.Length == 0 && curDialog.Length == 0) TextScript.PopUpThought("...");
					//start the thought at interaction thoughtIter
					StartingThought(thoughtIter);
					//update thoughtIter
					if (thoughtIter < curArrayOfThoughts.Length - 1) 
					{
						thoughtIter ++;
					}
					else thoughtIter = 0;
				}
				//make text disappear and press Button appear
				else if (TextScript.showingText)
				{	
					PlayerBasicMove.unstopPlayer();
					TextScript.DismissText();
					TextScript.PopUpPressButton();
				}
			}
			if(Input.GetKeyDown ("q"))
			{
				//initialization of the array of thoughts base on the current state
				for( int _i = 0; _i < dialogByState.Length; _i++)
				{
					if (dialogByState[_i].state == curState) curDialog = dialogByState[_i].dialog;
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
					if(curDialog.Length == 0) TextScript.PopUpDialog("mc:","...");
					else StartingDialog(dialogIter, speechIter);

					//update dialogIter
					if(!moreSpeech)
					{
						Debug.Log("l'ultima iterazione dovrei passare di qui");
						if (dialogIter < curDialog.Length - 1) 
						{
							dialogIter ++;
						}
						else dialogIter = 0;

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
						else dialogIter = 0;

					}
				}
				//make text disappear and press Button appear
				else if (TextScript.showingText)
				{	
					PlayerBasicMove.unstopPlayer();
					TextScript.DismissText();
					TextScript.PopUpPressButton();
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
		Debug.Log(_si);
		Debug.Log(curDialog[_di].Speakers.Length - 1);
		if(_si == speechLength){
			Debug.Log("sono qui!");
			moreSpeech = false;
			Debug.Log(moreSpeech);
		}
		else
		{	
			Debug.Log("sono nell'else");
			moreSpeech = true;
			speechIter ++;
			Debug.Log(moreSpeech);
		}
	}

	void StartingThought(int _i)
	{
		TextScript.PopUpThought(curArrayOfThoughts[_i]);

	}


	void StartingTimedthought(int _i)
	{
		TextScript.PopUpTimedThought(arrayOfTimedThoughts[_i]);
		timeDelay(time);
		TextScript.DismissText();
	}

	IEnumerator timeDelay(int _t)
	{
		yield return new WaitForSeconds(_t);
	}
}