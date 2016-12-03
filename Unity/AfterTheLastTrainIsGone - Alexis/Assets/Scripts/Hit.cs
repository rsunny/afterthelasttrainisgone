using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	public GameObject m_playerManagerGameObject;
	public GameObject m_healthGameObject;
	public GameObject m_counterAttackGameObject;
	public Animator m_playerAnimator;

	public float m_invulnerabilityDuration;
	public float m_disableActionDuration;

	bool m_defenseMode = false;
	bool m_invulnerability = false;

	int m_disableActionCoroutineCounter;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the defend mode is not activated, reduces the health by the damage given in argument and activate the corresponding animation
	public void GetHit(int damage){
		if (!m_defenseMode && !m_invulnerability) {
			
			m_healthGameObject.GetComponent<Health> ().SubstractHealth (damage);
			if (m_playerAnimator != null) {
				m_playerAnimator.SetTrigger ("GetHit");
			}
			StartCoroutine (Invulnerability ());
			if (m_playerManagerGameObject != null) {
				m_disableActionCoroutineCounter += 1;
				StartCoroutine (DisableAction ());
			}
		}
		if (m_counterAttackGameObject != null) {
			m_counterAttackGameObject.GetComponent<CounterAttack> ().GotHit ();
		}
	}

	// Sets the m_defendMode boolean to the value given in argument
	public void DefenseMode(bool defenseMode){
		m_defenseMode = defenseMode;
	}

	IEnumerator Invulnerability (){
		m_invulnerability = true;
		if (m_playerAnimator != null) {
			m_playerAnimator.SetBool ("Invulnerability", true);
		}
		yield return new WaitForSeconds (m_invulnerabilityDuration);
		m_invulnerability = false;
		if (m_playerAnimator != null) {
			m_playerAnimator.SetBool ("Invulnerability", false);
		}
	}

	IEnumerator DisableAction (){
		int numberCoroutine = m_disableActionCoroutineCounter;
		m_playerManagerGameObject.GetComponent<PlayerManager> ().DisableAction (true);
		yield return new WaitForSeconds (m_disableActionDuration);
		if (numberCoroutine == m_disableActionCoroutineCounter) {
			m_playerManagerGameObject.GetComponent<PlayerManager> ().DisableAction (false);
		}
	}
}
