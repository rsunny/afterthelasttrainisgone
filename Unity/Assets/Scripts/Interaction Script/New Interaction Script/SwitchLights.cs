using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLights : MonoBehaviour {

	public GameObject[] lights;
	public bool lightsOn = false;
	public string switchOnLine = " press E to switch on";
	public int timeSwitchOn = 1;
	public string switchOffLine= "press E to switch off";
	public int timeSwitchOff = 0;
	public string button = "Fire3";
	private int length = 0;
	
	public string lightsKey = "lights";
	private bool lightsAreOn;

	private void switchOn()
	{
		for(int _i = 0; _i < length; _i++)
		{
			if(_i == 0) lights[_i].SetActive(true);
			else StartCoroutine(switchOnLight(_i));
		}

	}
	private IEnumerator switchOnLight(int _i)
	{
		yield return new WaitForSeconds(timeSwitchOn);
		lights[_i].SetActive(true);
	}

	private void switchOff()
	{
		for(int _i = length - 1; _i >= 0; _i--)
		{
			if (_i == length - 1 ) lights[_i].SetActive(false);
			else StartCoroutine(switchOffLight(_i));
		}
	}
	private IEnumerator switchOffLight(int _i)
	{
		yield return new WaitForSeconds(timeSwitchOff);
		lights[_i].SetActive(false);
	}


	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player" )
		{
			//show the botton
			//initialized the Variables
			

			//dismiss previous text form narration or other colliders
			CanvasManager.DismissAll();
			
			string currentLine = "press E to interact";

			if(lightsOn)
			{
				CanvasManager.ShowButton(switchOffLine, false);	
			}
			else 
			{
				CanvasManager.ShowButton(switchOnLine, true);
			}


		}

	}

	void OnTriggerStay (Collider collideObj)
	{
		if (collideObj.tag == "Player" && Input.GetButtonDown (button))
		{
			CanvasManager.DismissButton();
			if(lightsOn)
			{
				switchOff();
				lightsOn = false;
				CanvasManager.ShowButton(switchOnLine, true);


			}
			else if (!lightsOn)
			{
				switchOn();
				lightsOn = true;
				CanvasManager.ShowButton(switchOffLine, false);
			}
		}
	}
	
	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player" )
		{
			CanvasManager.DismissAll();

		}
	
	}

	// Use this for initialization
	void Start () 
	{
		length = lights.Length;
		Debug.Log(ApplicationModel.GetString(lightsKey));
		if(ApplicationModel.GetString(lightsKey) == null) lightsAreOn = false;
		else
		{
			string lightsBool = ApplicationModel.GetString(lightsKey);
			Debug.Log(lightsBool);
			if( lightsBool == "true") lightsAreOn = true;
			else if( lightsBool == "false" ) lightsAreOn = false;
			
		}
		for(int _i = 0; _i < length; _i++)
		{
			lights[_i].SetActive(lightsAreOn);
		}


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
