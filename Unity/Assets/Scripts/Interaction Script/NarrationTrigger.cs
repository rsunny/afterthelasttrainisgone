using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour {

	private bool firstUse = true;
	public int time = 1;
	public string[] arrayOfTimedThoughts;
	private int timedThoughtIter = 0;

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			//dismiss all preavious tests
			TextScript.DismissText();
			//default case		
			if (arrayOfTimedThoughts.Length == 0) return;
			//start the thought at interaction timedThoughtIter
			if( firstUse)
			{
				StartingTimedthought(timedThoughtIter);
				firstUse = false;
			} 	
			//update timedThoughtIter
			if (timedThoughtIter < arrayOfTimedThoughts.Length - 1) 
			{
				timedThoughtIter ++;
			}
			else timedThoughtIter = 0;
	
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
