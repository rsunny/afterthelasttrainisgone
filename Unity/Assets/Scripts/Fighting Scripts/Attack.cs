using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	// The attack has an attack duration and an attack cooldown, it cannot attack if the previous attack is not done
	// Activate the attack animation of the animator attribute which the speed is set according  to the m_duration attribute
	// If it is in an ennemy collider, hit him calling an external method.


	public GameObject m_playerManagerGameObject;

	//SOUND
	public AudioClip m_punchSound;
	public AudioSource m_audioSource;

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

	public bool m_stoppable; //Able to be stopped by someone else attack

	// enemy gameobject tag 
	public string m_enemyTag; 

	// animator to call when attack
	public Animator m_weaponAnimator;
	public string m_animation;

	public float m_gotHitTimeWindow;

	protected bool m_gotHit;
	protected bool m_input = false;
	protected bool m_attackReady = true; // Is the weapon ready to attack 
	bool m_hitMode = false; // Is the attack hit enable
	int m_gotHitCoroutineCounter;

	IEnumerator coroutine; //used to store the attack coroutine, that allows to stop it !

	// Use this for initialization
	void Start () {
		if (m_weaponAnimator!=null){
			m_weaponAnimator.SetFloat("AttackSpeed", 1f/m_duration); // Sets the speed of the animator according to the attack speed
		}
		coroutine = AttackCoroutine (); // Set coroutine to a non-null value
		if (!Mathf.Approximately(m_setTime + m_hitTime + m_resetTime, 1f)) {
			Debug.Log ("The sum of the set, hit and reset times has to be equal to 1", gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (m_input && m_attackReady){
			m_attackReady = false; // Avoids to attack before the previous attack routine is done
			coroutine = AttackCoroutine (); //Reset coroutine to a new instantiation of attackcoroutine
			StartCoroutine (coroutine); // start the coroutine
		}
		if (m_gotHit && m_stoppable) {
			StopAttack (coroutine); // Stop the current value of coroutine
		}


	}

	public void Input (bool input){
		m_input = input;
	}

	void StopAttack(IEnumerator coroutine){
		StopCoroutine (coroutine);
		m_attackReady = true;
		m_playerManagerGameObject.GetComponent<PlayerManager> ().SetAttackMode (false);
		m_hitMode = false;
	}



	// set the attackmode of the gameobject to true for a fixed amount of time
	protected IEnumerator AttackCoroutine (){
		m_playerManagerGameObject.GetComponent<PlayerManager> ().SetAttackMode (true);
		if (m_weaponAnimator != null) {
			m_weaponAnimator.SetTrigger(m_animation);
		}
		if (m_audioSource != null) {
			m_audioSource.PlayOneShot (m_punchSound);
		}
		yield return new WaitForSeconds (m_duration * m_setTime); // Duration of the setting of the attack
		m_hitMode = true;
		yield return new WaitForSeconds (Mathf.Max (m_duration * m_hitTime, 1f / 30f)); // Duration of the hit (superior to 1 30fps-frame)
		m_hitMode = false;
		yield return new WaitForSeconds (m_duration * m_resetTime); // Duration of the resetting of the attack 
		yield return new WaitForSeconds (m_coolDown); // Waiting time until a new attack is possible
		m_attackReady = true; // At the end of the attack routine, a new attack is ready to be perform
		m_playerManagerGameObject.GetComponent<PlayerManager> ().SetAttackMode (false);


	}

	public void GotHit (){
		m_gotHitCoroutineCounter += 1;
		StartCoroutine (GotHitCoroutine ());
	}

	IEnumerator GotHitCoroutine (){
		int numberCoroutine = m_gotHitCoroutineCounter;
		m_gotHit = true;
		yield return new WaitForSeconds (Mathf.Max(m_gotHitTimeWindow,1f/30f)); // ensure the time the gotHit boolean is true is superioir to the duration of a frame
		if (numberCoroutine == m_gotHitCoroutineCounter) { //will not change the gotHit boolean if an other coroutine has been launched
			m_gotHit = false;
		}
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
