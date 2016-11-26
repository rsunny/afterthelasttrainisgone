using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	// Key = Jump
	// Allow the player to jump once when is grounded according to the m_jumpHeight attribute

	[Range(0f,25f)]
	public float m_jumpHeight = 10f;

	Rigidbody rb;
	Transform tr;

	bool m_jump = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> () as Rigidbody;
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		// On input, if the player is grounded, he will jump (see FixedUpdate)
		if (Input.GetButtonDown("Jump") && IsGrounded()){
			m_jump=true;
		}

	}

	void FixedUpdate () {
		// jump update
		if (m_jump) {
			rb.AddForce (m_jumpHeight * Vector3.up, ForceMode.Impulse);
			m_jump = false;
		}
	}


	// Use a Raycast to determine the distance between the gameobject and the closest box collider beneath
	// If the distance is small enough, we say that the gameobject is grounded, thus it is allow to jump
	bool IsGrounded(){
		RaycastHit hit;
		bool grounded = false;
		if (Physics.Raycast (tr.position, -Vector3.up, out hit)) {
			if (hit.distance < 0.1f) { //not set 0 allows to avoid some bugs
				grounded = true;
			}
		}
		return grounded;
	}

}
