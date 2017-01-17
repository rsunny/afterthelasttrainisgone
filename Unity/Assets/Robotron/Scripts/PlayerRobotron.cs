using UnityEngine;
using System.Collections;

public class PlayerRobotron : MonoBehaviour {
	
	Transform tr; 

	[Header("Shooting Directions")]
	public Transform m_shootright;
	public Transform m_shootleft;
	public Transform m_shootup;
	public Transform m_shootdown;

	[Space(10)]
	public GameObject m_shot_prefab;

	[Range(25f,300f)]
	public float m_speed = 50f;

	float m_horizontal = 0f;
	float m_vertical = 0f;

	private string shootDown = "ShootDown";
	private string shootUp = "ShootUp";
	private string shootLeft = "ShootLeft";
	private string shootRight = "ShootRight";

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");

		if (Input.GetButtonDown (shootRight)) {
			// shoot right!
			GameObject go = Instantiate (m_shot_prefab,
				                m_shootright.position,
				                m_shootright.rotation) 
				as GameObject;

			SoundManager.Instance.PlayerShoots ();
		}

		if (Input.GetButtonDown (shootLeft)) {
			// shoot right!
			GameObject go = ObjectPoolingManager.Instance.GetObject(m_shot_prefab.name);
			go.transform.position = m_shootleft.position;
			go.transform.rotation = m_shootleft.rotation;
//				Instantiate (m_shot_prefab,
//				m_shootleft.position,
//				m_shootleft.rotation) 
//				as GameObject;
			SoundManager.Instance.PlayerShoots ();
		}


		if (Input.GetButtonDown (shootUp)) {
			// shoot right!
			GameObject go = Instantiate (m_shot_prefab,
				m_shootup.position,
				m_shootup.rotation) 
				as GameObject;
			SoundManager.Instance.PlayerShoots ();
		}

		if (Input.GetButtonDown (shootDown)) {
			// shoot right!
			GameObject go = Instantiate (m_shot_prefab,
				m_shootdown.position,
				m_shootdown.rotation) 
				as GameObject;
			SoundManager.Instance.PlayerShoots ();
		}
	}

	void FixedUpdate() {
		tr.position = tr.position +
		m_horizontal * transform.right
			* m_speed * Time.fixedDeltaTime +
		m_vertical * transform.up
			* m_speed * Time.fixedDeltaTime;
	}
}
