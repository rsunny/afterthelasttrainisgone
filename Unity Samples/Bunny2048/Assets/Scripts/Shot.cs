using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public float m_speed = 200f;
	public GameObject m_blood_mark;

	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tr.position = tr.position +
		tr.right * m_speed * Time.fixedDeltaTime;
        Vector2 camera_boundaries = GameManager.Instance.CameraBoundaries();
        float width = camera_boundaries.x;
        float height = camera_boundaries.y;

        if (Mathf.Abs (tr.position.x) > width)
			gameObject.SetActive(false);
		if (Mathf.Abs (tr.position.y) > height)
			gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy")
        {
            Debug.Log("HIT " + other.gameObject.name);
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_blood_mark.name);
            Vector3 position = other.gameObject.transform.position;
            position.z = 1;
            go.transform.position = position;
            go.transform.rotation = other.gameObject.transform.rotation;
			SoundManager.Instance.EnemyDies();
            other.gameObject.SetActive(false);
            GameManager.Instance.Scored(100);
            gameObject.SetActive(false);
        }
	}
}
