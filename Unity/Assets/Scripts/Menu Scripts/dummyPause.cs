using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyPause : MonoBehaviour {

	// Use this for initialization
	void Start() {
        Debug.Log("in awake");
        DontDestroyOnLoad(gameObject);
	}
}
