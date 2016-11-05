using UnityEngine;
using System.Collections;


public class FakeEnemy : MonoBehaviour {

	private Animator m_animator;
	Transform tr; 
	int flag;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> () as Animator;
		tr = GetComponent<Transform> () as Transform;
		flag = 1;
	}

	// Update is called once per frame
	void Update () {
		print (flag);
		Vector3 player_position = Player.Instance.GetPosition ();

		if ((tr.position.x > player_position.x) && (flag==2)) {
			m_animator.SetInteger ("Direction", 1);
			flag = 1;
		}
		if ((tr.position.x <= player_position.x) && (flag==1)) {
			m_animator.SetInteger ("Direction", 2);
			flag = 2;
		}


	}
}
