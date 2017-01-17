using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public GameObject m_playerManagerGameObject;

	public int m_health;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		/*if (m_health <= 0) {
			m_playerManagerGameObject.GetComponent<PlayerManager> ().Die ();
		}*/
	}

	public void SubstractHealth(int damage){
		m_health -= damage;
	}


}