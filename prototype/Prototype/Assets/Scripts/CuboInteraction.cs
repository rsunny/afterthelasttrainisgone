using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboInteraction : MonoBehaviour {

	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			PressButtonInteraction.PopUpText();
		}
	}
	void OnTriggerStay (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			if(Input.GetKeyDown ("e"))
			{
				if (!InteractionsScript.showingInteraction)
				{
					PressButtonInteraction.DismissText();
					StartingInteraction();
				}
				else if (InteractionsScript.showingInteraction)
				{
					InteractionsScript.DismissText();
					PressButtonInteraction.PopUpText();
				}
			}


			Debug.Log("sto dentro");
		}
	}
	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			PressButtonInteraction.DismissText();
			InteractionsScript.DismissText();

		}
	}

	void StartingInteraction()
	{
		InteractionsScript.PopUpInteraction("",
			"I like this Painting \n but I don't know why is here", 
			InteractionsScript.InteractionType.Thought, 
			DoNothing);
	}

	void DoNothing(){
		if(Input.GetKeyDown ("e"))
		{
		InteractionsScript.DismissText();
		}
		Debug.Log("Non faccio nulla");
	}
}
