using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public GameObject m_playerManagerGameObject;

	// horizontal & vertical input variables
	//float m_horizontal = 0f;
	//float m_depth = 0f;

	//bool m_jumpInput;
	bool m_attackInput;
	//bool m_interactionInput;
	bool m_defenseInput;
	bool m_counterAttackInput;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (m_playerManagerGameObject != null) {
			// Move
			//m_horizontal = Input.GetAxis ("Horizontal");
			//m_depth = Input.GetAxis ("Vertical");
			//m_playerManagerGameObject.GetComponent<PlayerManager> ().Move (m_horizontal, m_depth);

			//Jump
			//m_jumpInput = Input.GetButtonDown ("Jump");
			//m_playerManagerGameObject.GetComponent<PlayerManager> ().Jump (m_jumpInput);

			//Attack
			m_attackInput = Input.GetButtonDown ("Fire3");
			m_playerManagerGameObject.GetComponent<PlayerManager> ().Attack (m_attackInput);

			//Interaction
			//m_interactionInput = Input.GetButtonDown ("Fire2");
			//m_playerManagerGameObject.GetComponent<PlayerManager> ().Interaction (m_interactionInput);

			//Defense
			m_defenseInput = Input.GetButton ("Jump");
			m_playerManagerGameObject.GetComponent<PlayerManager> ().Defense (m_defenseInput);

			//CounterAttack
			m_counterAttackInput = Input.GetButtonDown ("Jump");
			m_playerManagerGameObject.GetComponent<PlayerManager> ().CounterAttack (m_counterAttackInput);
		}
	}
}