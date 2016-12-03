using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject m_moveGameObject;
	public GameObject m_jumpGameObject;
	public GameObject m_interactionGameObject;
	public GameObject m_defenseGameObject;
	public GameObject m_attackGameObject;
	public GameObject m_counterAttackGameObject;

	bool m_attackMode = false;
	bool m_defenseMode = false;
	bool m_disableAction = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Move(float horizontalInput, float depthInput){
		if (!m_disableAction && !m_defenseMode && m_moveGameObject != null) {
			m_moveGameObject.GetComponent<Move> ().Input (horizontalInput, depthInput);
		} else {
			m_moveGameObject.GetComponent<Move> ().Input (0, 0);
		}
	}

	public void Jump(bool jumpInput){
		if (m_jumpGameObject != null) {
			m_jumpGameObject.GetComponent<Jump> ().Input (!m_disableAction && jumpInput && !m_defenseMode);
		}
	}

	public void Interaction(bool interactionInput){
		if (m_interactionGameObject != null) {
			m_interactionGameObject.GetComponent<Interaction> ().Input (!m_disableAction && interactionInput);
		}
	}

	public void Defense(bool defenseInput){
		if (m_defenseGameObject != null) {
			m_defenseGameObject.GetComponent<Defense> ().Input (!m_disableAction && defenseInput && !m_attackMode);
		}
	}

	public void Attack(bool attackInput){
		if (m_attackGameObject != null) {
			m_attackGameObject.GetComponent<Attack> ().Input (!m_disableAction && attackInput && !m_attackMode && !m_defenseMode);
		}
	}

	public void CounterAttack(bool counterAttackInput){
		if (m_counterAttackGameObject != null) {
			m_counterAttackGameObject.GetComponent<CounterAttack> ().Input (!m_disableAction && counterAttackInput && !m_attackMode);
		}
	}

	public void Die(){
		Destroy (gameObject);
	}

	public void SetAttackMode(bool attackMode){
		m_attackMode = attackMode;
	}

	public void SetDefenseMode(bool defenseMode){
		m_defenseMode = defenseMode;
	}

	public void DisableAction(bool disableAction){
		m_disableAction = disableAction;
	}
}
