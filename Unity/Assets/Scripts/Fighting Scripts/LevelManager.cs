using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	int m_enemyNumber;
	bool lol;
	public GUIStyle m_guiStyle;
	public GUIStyle m_guiStyle2;
	public GUIContent m_text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_enemyNumber = GameObject.FindGameObjectsWithTag ("Enemy").Length;
	}

	void OnGUI() {
		if (m_enemyNumber == 0) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Thanks for having played this game demo,\n we hope you enjoyed it ! \n \n Don't hesitate to give us some feedback :)", m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Thanks for having played this game demo,\n we hope you enjoyed it ! \n \n Don't hesitate to give us some feedback :)", m_guiStyle);
		}
	}
}
