using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player Instance = null;
    Transform tr; 

	[Header("Shooting Directions")]
	public Transform m_shootright;
	public Transform m_shootleft;
	public Transform m_shootup;
	public Transform m_shootdown;

	[Space(10)]
	public GameObject m_shot_prefab;

	[Range(25f,1000f)]
	public float m_speed = 500f;
    public float m_horizontal_position = 0f;
    public float m_vertical_position = 0f;

	float m_horizontal = 0f;
	float m_vertical = 0f;

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
        tr.position = m_horizontal_position * transform.right +
             m_vertical_position * transform.up;

    }
	
	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");

		if (Input.GetKeyDown (KeyCode.L)) {
            // shoot right!
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_shot_prefab.name);
            go.transform.position = m_shootright.position;
            go.transform.rotation = m_shootright.rotation;
            SoundManager.Instance.PlayerShoots ();
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			// shoot left!
			GameObject go = ObjectPoolingManager.Instance.GetObject(m_shot_prefab.name);
			go.transform.position = m_shootleft.position;
			go.transform.rotation = m_shootleft.rotation;
			SoundManager.Instance.PlayerShoots ();
		}


		if (Input.GetKeyDown (KeyCode.I)) {
            // shoot up!
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_shot_prefab.name);
            go.transform.position = m_shootup.position;
            go.transform.rotation = m_shootup.rotation;
            SoundManager.Instance.PlayerShoots ();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
            // shoot down!
            GameObject go = ObjectPoolingManager.Instance.GetObject(m_shot_prefab.name);
            go.transform.position = m_shootdown.position;
            go.transform.rotation = m_shootdown.rotation;
            SoundManager.Instance.PlayerShoots ();
		}
	}

	void FixedUpdate() {
		tr.position = tr.position +
		m_horizontal * transform.right
			* m_speed * Time.fixedDeltaTime +
		m_vertical * transform.up
			* m_speed * Time.fixedDeltaTime;

        // keeps the player inside of the camera field
        Vector2 camera_boundaries = GameManager.Instance.CameraBoundaries();
        float width = camera_boundaries.x;
        float height = camera_boundaries.y;
        if (tr.position.x > width)
        {
            Vector3 position_tmp = tr.position;
            position_tmp.x = width;
            tr.position = position_tmp;
        }
        if (tr.position.x < -width)
        {
            Vector3 position_tmp = tr.position;
            position_tmp.x = -width;
            tr.position = position_tmp;
        }
        if (tr.position.y > height)
        {
            Vector3 position_tmp = tr.position;
            position_tmp.y = height;
            tr.position = position_tmp;
        }
        if (tr.position.y <- height)
        {
            Vector3 position_tmp = tr.position;
            position_tmp.y = -height;
            tr.position = position_tmp;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("PLAYER HIT" + other.gameObject.name);
            SoundManager.Instance.PlayerDies();
            GameManager.Instance.GameOver();
        }
    }

	public Vector3 GetPosition(){
		return tr.position;
	}
}
