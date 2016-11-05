using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player Instance = null;
	Transform tr; 

	[Range(0f,25f)]
	public float m_speed = 2f;
	public float m_horizontal_position = 0f;
	public float m_depth_position = 0f;
	[Range(0f,4f)]
	public float m_depth_coefficient = 1f;

	float m_horizontal = 0f;
	float m_depth = 0f;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	public void Start () {
		tr = GetComponent<Transform> () as Transform;
		//tr.position = m_horizontal_position * transform.right +
		//m_depth_position * transform.forward;  

	}

	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_depth = Input.GetAxis ("Vertical");
	}

	void FixedUpdate() {
		tr.position = tr.position +
			m_horizontal * transform.right
			* m_speed * Time.fixedDeltaTime +
			m_depth * transform.forward
			* m_speed * Time.fixedDeltaTime * m_depth_coefficient;

		// keeps the player inside of the camera field

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("PLAYER HIT" + other.gameObject.name);
	}
		
}
