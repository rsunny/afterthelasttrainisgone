using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Allow the player to move according to the m_speed attribute and the axis input

	[Range(0f,25f)]
	public float m_speed = 10f;

	// horizontal & vertical input variables
	float m_horizontal = 0f;
	float m_depth = 0f;

	bool m_facingRight;

	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}

	// Update is called once per frame
	void Update () {
		// Flip the sprite according to the horizontal direction of the deplacement
		if ((m_horizontal>0 && !m_facingRight) || (m_horizontal<0 && m_facingRight)){
			Flip();
		}
	}

	void FixedUpdate () {
		// position update
		tr.position += 	m_horizontal * Vector3.right* m_speed * Time.fixedDeltaTime +
			m_depth * Vector3.forward	* m_speed * Time.fixedDeltaTime;
	}

	public void Input (float horizontal, float depth){
		m_horizontal = horizontal;
		m_depth= depth;
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing
		m_facingRight = !m_facingRight;

		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
