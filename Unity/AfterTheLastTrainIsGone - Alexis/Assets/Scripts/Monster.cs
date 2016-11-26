using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public int m_health;

	public Animator m_animator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (m_health <= 0) {
			DestroyMonster ();
		}
	}


	public void GetHit(int damage){
		m_health -= damage;
		m_animator.SetTrigger ("GetHit");
	}

	public void DestroyMonster(){
		Destroy (gameObject);
	}
}
