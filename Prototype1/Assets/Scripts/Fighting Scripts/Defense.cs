using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour {

	public GameObject m_hitGameObject;
	public GameObject m_playerManagerGameObject;

	public Animator m_playerAnimator;
	public Animator m_swordAnimator;

	bool m_defenseMode = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		DefenseAnimation ();
		m_hitGameObject.GetComponent<Hit> ().DefenseMode (m_defenseMode);
		m_playerManagerGameObject.GetComponent<PlayerManager> ().SetDefenseMode (m_defenseMode);
	}

	void DefenseAnimation(){
		if (m_defenseMode) {
			if (m_playerAnimator != null) {
				m_playerAnimator.SetBool ("Defense",true);
			}
			if (m_swordAnimator != null) {
				m_swordAnimator.SetBool ("Defense",true);
			}
		} else {
			if (m_playerAnimator != null) {
				m_playerAnimator.SetBool ("Defense",false);
			}
			if (m_swordAnimator != null) {
				m_swordAnimator.SetBool ("Defense",false);
			}
		}
	}

	public void Input(bool input){
		m_defenseMode = input;
	}
		
}
