using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInteractionScript : MonoBehaviour {

	private int thougthtIter = 0;
	private int dialogIter = 0;
	
	public string[] arrayOfThoughts;

	[System.Serializable]
	public struct Line
	{
		public string[] Speakers;
		public string[] Speech;
	};
	public Line[] dialog;
	private int speechIter = 0;
	private bool moreSpeech = false;



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
			//thougth
			if(Input.GetKeyDown ("e"))
			{	
				//make text appear
				if (!TextScript.showingText)
				{	
					

					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					//default case
					if (arrayOfThoughts.Length == 0 && dialog.Length == 0) TextScript.PopUpThought("...");
					//start the thougth at interaction thougthtIter
					StartingThought(thougthtIter);
					//update thougthtIter
					if (thougthtIter < arrayOfThoughts.Length - 1) 
					{
						thougthtIter ++;
					}
					else thougthtIter = 0;
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
				if(!TextScript.showingText)
				{	
					speechIter = 0;
					//block the player movment while text appearing
					PlayerBasicMove.stopPlayer();
					//dismiss all preavious tests
					TextScript.DismissText();
					//start the thougth at interaction dialogIter
					if(dialog.Length == 0) TextScript.PopUpDialog("mc:","...");
					else StartingDialog(dialogIter, speechIter);

					//update dialogIter
					if(!moreSpeech)
					{
						Debug.Log("l'ultima iterazione dovrei passare di qui");
						if (dialogIter < dialog.Length - 1) 
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
						Debug.Log("l'ultima iterazione dovrei passare di qui");
						if (dialogIter < dialog.Length - 1) 
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
			TextScript.DismissText();
		}
	}


	void StartingDialog(int _di, int _si)
	{	
		int speechLength = dialog[_di].Speakers.Length - 1;
		TextScript.PopUpDialog(dialog[_di].Speakers[_si],dialog[_di].Speech[_si]);
		Debug.Log(_si);
		Debug.Log(dialog[_di].Speakers.Length - 1);
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
		TextScript.PopUpThought(arrayOfThoughts[_i]);
	}

}