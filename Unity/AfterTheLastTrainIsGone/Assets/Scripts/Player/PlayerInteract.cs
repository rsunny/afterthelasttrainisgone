using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

	// Allow the player to interact with "NPC" tagged gameobject (on trigger) - key = Fire1

	// interaction vairbale 
	GameObject m_interactingNPC;
	bool m_inNPCTrigger=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Interaction with NPC tagged gameobject
		// On input, if the player is in a NPCtrigger, he will interact with the NPC
		// Call of an external method
		if (Input.GetButtonDown ("Fire2") && m_inNPCTrigger) {
			m_interactingNPC.GetComponent<TextBox> ().PlayerInteraction ();
		}
	}

	// On trigger, set the potential interacting NPC and the NPCtrigger flag
	// Will create conflicts if in two different NPC colliders
	void OnTriggerEnter(Collider other){
		if (other.tag == "NPC") {
			m_interactingNPC = other.gameObject;
			m_inNPCTrigger=true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "NPC") {
			other.gameObject.GetComponent<TextBox>().DestroyTextBox();
			m_inNPCTrigger = false;
		}
	}
}
