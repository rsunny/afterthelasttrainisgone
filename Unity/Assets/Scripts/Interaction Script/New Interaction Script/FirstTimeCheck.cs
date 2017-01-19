using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeCheck : MonoBehaviour {


	public static bool firstTime = true;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
