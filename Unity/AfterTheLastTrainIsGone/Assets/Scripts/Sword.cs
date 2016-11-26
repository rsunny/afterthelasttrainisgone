using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int m_damage;
	public float m_attackDuration;
	public float m_attackCoolDown;

	bool m_attackMode = false;
	bool m_attackReady = true;

	Animator m_animator;


	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> ();
		m_animator.SetFloat("AttackSpeed", 1f/m_attackDuration);
			
	}
	
	// Update is called once per frame
	void Update () {	
	}

	public void Attack(){
		if (m_attackReady) {
			m_attackReady = false;
			StartCoroutine (AttackCoroutine ());
		}
	}

	// set the attackmode of the gameobject to true for a fixed amount of time
	IEnumerator AttackCoroutine (){
		m_attackMode = true;
		m_animator.SetTrigger ("Attack");
		yield return new WaitForSeconds (m_attackDuration);
		m_attackMode = false;
		yield return new WaitForSeconds (m_attackCoolDown);
		m_attackReady = true;

	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" && m_attackMode){
			other.gameObject.GetComponent<Monster>().GetHit(m_damage);
			m_attackMode = false; // reset the attackMode to false to prevent to hit more than once per attack
		}
	}
}
