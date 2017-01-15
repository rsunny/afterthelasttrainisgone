using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour {

	public GameObject m_playerManagerGameObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_playerManagerGameObject.GetComponent<PlayerManager> ().Attack (true);
	
	}
}
