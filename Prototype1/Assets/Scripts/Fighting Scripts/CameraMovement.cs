using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public bool m_xFixed;
	public bool m_yFixed;
	public bool m_zFixed;

	public GameObject player;

	Vector3 m_offset;
	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_offset = tr.position - player.transform.position;

	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPosition = new Vector3(0, 0,0);
		if (!m_xFixed) {
			newPosition += player.transform.position.x * Vector3.right;

		}
		if (!m_yFixed) {
			newPosition += player.transform.position.y * Vector3.up;
		}
		if (!m_zFixed) {
			newPosition += player.transform.position.z * Vector3.forward;
		}
		tr.position = m_offset + newPosition;
	}

	void FixedUpdate(){
		
	}
}
