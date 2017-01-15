using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidBodyMovement : MonoBehaviour {

	//instance of player
	public static PlayerRigidBodyMovement Instance = null;

	//animarion
	Animator anim;
	
	//trasform
	Transform tr;

	float h = 0f;
	float v = 0f;

	[Range(0f,25f)]
	public float speed = 2f;

	private Rigidbody playerRigidbody;
	private Vector3 movement;

	
	// Use this for initialization
	void Start () 
	{
		playerRigidbody = GetComponent <Rigidbody> ();
		anim = GetComponent<Animator> ();	
	}

	void FixedUpdate() 
	{
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		Move(h,v,speed);
		Animating(h,v);

	}

	void Move(float _h, float _v, float _speed)
    {
        // Set the movement vector based on the axis input.
        movement.Set (_h, 0f, _v);
        
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * _speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition (transform.position + movement);
    }

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

	void Animating (float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool ("IsWalking", walking);

		bool walkingR = h > 0;
		bool walkingL = h < 0;

		anim.SetBool("WalkingL", walkingL);


		/* 
		animation are managed by an integer set from 0 to 8
		how it is work:
			
	
					3  
				 4  |  2
			    5 --0-- 1
				 6  |  8			|
					7

		*/
		int movingDirection = 0;

		//right
		if (h > 0){

			if (v > 0) movingDirection = 2;//is walking up-right

			else if (v == 0) movingDirection = 1;//is walking right

			else if (v < 0) movingDirection = 8;//is walking down-right

		}

		//left
		else if (h < 0){

			if (v > 0) movingDirection = 4;//is walking up-left

			else if (v == 0) movingDirection = 5;//is walking left

			else if (v < 0) movingDirection = 6;//is walking down-left
		}

		//centre
		else if (h == 0){

			if (v > 0) movingDirection = 3;//is walking up

			else if (v == 0) movingDirection = 0;//is not walking

			else if (v < 0) movingDirection = 7;// is walking down

		}

		//general case for any mistake
		else 
				{
					Debug.Log("movement direction animation mistake!");
				}
			

	    anim.SetInteger ("movingDirection", movingDirection);

		//case walkingR
		/*
		bool walkingR = h > 0 && v = 0;

		bool walkingUpR = h > 0 && v > 0;

		bool walkingUp = h = 0 && v > 0;

		bool walkingUpL = h < 0 && v > 0;

		bool walkingL = h < 0 && v  = 0;

		bool walkingDownL = h < 0 && v < 0;

		bool walkingDown = h = 0 && v < 0;

		bool walkingDownR = h > 0 && v < 0;

		*/

		
    }
	// Update is called once per frame
	void Update () {
		
	}
}
