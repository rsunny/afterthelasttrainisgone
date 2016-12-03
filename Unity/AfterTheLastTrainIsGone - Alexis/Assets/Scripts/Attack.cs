using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	// The attack has an attack duration and an attack cooldown, it cannot attack if the previous attack is not done
	// Activate the attack animation of the animator attribute which the speed is set according  to the m_duration attribute
	// If it is in an ennemy collider, hit him calling an external method.


	public GameObject m_playerManagerGameObject;

	// weapon characteristics 

	// Normal attack parameters
	public int m_damage;
	public float m_duration; // the whole attack duration that encompasses the setTime, the hitTime and the resetTime
	// the addition of all those times has to be equal to 1
	[Range (0,1)]
	public float m_setTime;
	[Range (0,1)]
	public float m_hitTime;
	[Range (0,1)]
	public float m_resetTime;
	public float m_coolDown; // Time before an other attack can be launch after the previous one is done

	// enemy gameobject tag 
	public string m_enemyTag; 

	// animator to call when attack
	public Animator m_weaponAnimator;
	public string m_animation;

	protected bool m_input = false;
	protected bool m_attackReady = true; // Is the weapon ready to attack 
	bool m_hitMode = false; // Is the attack hit enable


	// Use this for initialization
	void Start () {
		if (m_weaponAnimator!=null){
			m_weaponAnimator.SetFloat("AttackSpeed", 1f/m_duration); // Sets the speed of the animator according to the attack speed
		}
	}

	// Update is called once per frame
	void Update () {
		if (m_input && m_attackReady){
			m_attackReady = false; // Avoids to attack before the previous attack routine is done
			m_playerManagerGameObject.GetComponent<PlayerManager> ().SetAttackMode (true);
			StartCoroutine (AttackCoroutine ());
			m_playerManagerGameObject.GetComponent<PlayerManager> ().SetAttackMode (false);
		}
			
	}

	public void Input (bool input){
		m_input = input;
	}


	// set the attackmode of the gameobject to true for a fixed amount of time
	protected IEnumerator AttackCoroutine (){
		

		if (m_weaponAnimator != null) {
			m_weaponAnimator.SetTrigger (m_animation);
		}

		yield return new WaitForSeconds (m_duration * m_setTime); // Duration of the setting of the attack
		m_hitMode = true;
		yield return new WaitForSeconds (Mathf.Max(m_duration * m_hitTime,1f/30f)); // Duration of the hit (superior to 1 30fps-frame)
		m_hitMode = false;
		yield return new WaitForSeconds (m_duration * m_resetTime); // Duration of the resetting of the attack 
		yield return new WaitForSeconds (m_coolDown); // Waiting time until a new attack is possible
		m_attackReady = true; // At the end of the attack routine, a new attack is ready to be perform


	}

	// If an attack occurs and the attack collider and the enemy collider overlap, the enemy get hit
	void OnTriggerStay(Collider other){
		if (other.tag == m_enemyTag){
			if (m_hitMode){
				other.gameObject.GetComponent<Hit>().GetHit(m_damage);
				m_hitMode = false; // reset the attackMode to false to prevent to hit more than once per attack
			}
		}
	}
}
