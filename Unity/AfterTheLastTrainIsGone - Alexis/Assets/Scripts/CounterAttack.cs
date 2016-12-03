using UnityEngine;
using System.Collections;

public class CounterAttack : Attack {

	public float m_gotHitTimeWindow;
	public float m_inputTimeWindow;

	bool m_gotHit;
	int m_gotHitCoroutineCounter;

	int m_inputCoroutineCounter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_input && m_gotHit && m_attackReady){
			m_attackReady = false; // Avoids to attack before the previous attack routine is done
			StartCoroutine (AttackCoroutine ());
		}
	}

	public void GotHit (){
		m_gotHitCoroutineCounter += 1;
		StartCoroutine (GotHitCoroutine ());
	}

	IEnumerator GotHitCoroutine (){
		int numberCoroutine = m_gotHitCoroutineCounter;
		m_gotHit = true;
		yield return new WaitForSeconds (m_gotHitTimeWindow);
		if (numberCoroutine == m_gotHitCoroutineCounter) { //will not change the gotHit boolean if an other coroutine has been launched
			m_gotHit = false;
		}
	}

	public void Input (bool input){
		if (input) {
			m_inputCoroutineCounter += 1;
			StartCoroutine (InputCoroutine ());
		}
	}

	// Set the input boolean to true for a fixed duration
	IEnumerator InputCoroutine (){
		int numberCoroutine = m_inputCoroutineCounter;
		m_input = true;
		yield return new WaitForSeconds (m_inputTimeWindow);
		if (numberCoroutine == m_inputCoroutineCounter) {
			m_input = false;
		}
	}
}
