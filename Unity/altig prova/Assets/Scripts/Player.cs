using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	public static Player Instance = null;



	public enum MovementType {A,B};
	public MovementType typeOfMovement = MovementType.A;



	[Range(0f,25f)]
	public float m_speed = 2f;
	
	
	//animarion
	Animator anim;

	//movement method 1
	Rigidbody playerRigidbody;
	Vector3 movement;
	
	//movement method 2
	Transform tr;
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
		playerRigidbody = GetComponent <Rigidbody> ();
		anim = GetComponent<Animator> ();
		tr = GetComponent<Transform> () as Transform;

		//tr.position = m_horizontal_position * transform.right +
		//m_depth_position * transform.forward;

	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_depth = Input.GetAxis ("Vertical");
		
		
		if (typeOfMovement ==  MovementType.A){
			Move1 (m_horizontal, m_depth);
		} else if(typeOfMovement ==  MovementType.B) {
			Move2 (m_horizontal,m_depth);
		} ;
		

		//animation
		Animating(m_horizontal,m_depth);
		
		// keeps the player inside of the camera field

	}

	void Move1 (float h, float v)
	{
			tr.position = tr.position +
			h * transform.right
			* m_speed * Time.fixedDeltaTime +
			v * transform.forward
			* m_speed * Time.fixedDeltaTime * m_depth_coefficient;
	}


    void Move2 (float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set (h, 0f, v);
        
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * m_speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition (transform.position + movement);
    }

    void Animating (float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool ("IsWalking", walking);

		bool walkingR = h > 0;
		bool walkingL = h < 0;
		
    }


}
