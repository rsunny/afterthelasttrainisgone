using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLightsOn : MonoBehaviour {
	public string lightKey = "lights";
	public GameObject SwitchLightObject;
	//public Component SwitchLightScript;
	private bool lightsOn;

	// Use this for initialization
	void Start () 
	{


		
	}
	
	// Update is called once per frame
	void Update () 
	{

		lightsOn = SwitchLightObject.GetComponent<SwitchLights>().lightsOn;
		if(lightsOn) ApplicationModel.SetString( lightKey,"true" );
		else if (!lightsOn)  ApplicationModel.SetString( lightKey,"false" );
		Debug.Log(lightsOn);
		Debug.Log(ApplicationModel.GetString( lightKey ));
	}
}
