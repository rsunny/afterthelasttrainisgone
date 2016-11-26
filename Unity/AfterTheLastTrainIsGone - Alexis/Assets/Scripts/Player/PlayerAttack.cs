using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	// Allow the player to attack with its m_sword attribute

	public GameObject m_sword;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Attack 
		// On input, if the player has a sword attribute, he will attack with it
		// Call of an external method
		if (Input.GetButtonDown("Fire1") && m_sword!=null){
			m_sword.GetComponent<Sword>().Attack();
		}
	}
}
