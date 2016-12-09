using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour {

/*THIS SCRIPT MUST BE PUT ON A TRIGGERED COLLIDER*/

	
	public enum InteractionType
    {
        Thought, //OkDialog,
        Dialog //Dialog
    };

	//the type of interaction that the object is gonna offer
	public InteractionType typpeOfInteractiveObject = InteractionType.Thought;

	//the state ingfluence the type of interactions you can have with this object
	public int state = 0;

	//modifier for the state
	public void upgradeState(){}
	public void downgradeState(){}
	public void setStateTo(int newState){}

	
	
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
					StartInteraction();
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

	void StartInteraction()
	{
			

	}
	




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
