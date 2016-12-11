using UnityEngine;
using System.Collections;

public class CounterAttack : Attack {

	public float m_inputTimeWindow;

	int m_inputCoroutineCounter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_input && m_gotHit && m_attackReady){
			m_attackReady = false; // Avoids to attack before the previous attack routine is done
			m_gotHit=false;
			m_input = false;
			StartCoroutine (AttackCoroutine ());
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
