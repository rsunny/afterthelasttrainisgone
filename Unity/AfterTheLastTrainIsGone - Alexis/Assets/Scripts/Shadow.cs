using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	// Set the height of the shadow to the closest collider beneath

	Transform tr;
	Transform trParent;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		trParent = tr.parent;
	}

	// Update is called once per frame
	void Update () {
		// Use a raycast from the parent gameobject to determine the closest collider beneath and associate the y value to the shadow
		if (Physics.Raycast (trParent.position, -Vector3.up, out hit)) {
			Vector3 position = new Vector3 (tr.position.x, hit.point.y + 0.01f, tr.position.z);
			tr.position = position;
		}
	}
}
