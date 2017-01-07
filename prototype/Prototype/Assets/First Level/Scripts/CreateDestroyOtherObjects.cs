using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroyOtherObjects : MonoBehaviour {


	public bool onEnter = true;
	public int seconds = 0;
	public GameObject objectToCreate = null;
	public GameObject objectToDestroy = null;


	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" && onEnter)
		{
			doAfterTime ();
		}
	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player" && !onEnter)
		{
			doAfterTime ();
		}
	}


public void doAfterTime ()
{
	StartCoroutine(waitAndOperate());
}

private IEnumerator waitAndOperate()
{
	yield return new WaitForSeconds(seconds);
	if (objectToDestroy != null)
	{
		objectToDestroy.SetActive(false);
	}
	if (objectToCreate != null)
	{
		objectToCreate.SetActive(true);
	}

}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

