using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLightsOnRobotron : MonoBehaviour {

	public string lightKey = "lights";
	private string lightsOn;

	// Use this for initialization
	void Start () 
	{
		lightsOn =	ApplicationModel.GetString(lightKey);
		ApplicationModel.SetString( lightKey, lightsOn );
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
