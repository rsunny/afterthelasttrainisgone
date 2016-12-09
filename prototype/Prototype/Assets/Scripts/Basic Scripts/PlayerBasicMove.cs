using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMove : MonoBehaviour {

	//instance of player
	public static PlayerBasicMove Instance = null;
	//bool to make it stop
	public static bool stop = false;

	//animarion
	Animator anim;
	
	//trasform
	Transform tr;

	float horiz = 0f;
	float vert = 0f;
	[Range(0f,25f)]
	public float speed = 2f;
	[Range(0f,4f)]
	public float vertCoef = 1f;
	

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
	void Start () 
	{
		tr = GetComponent<Transform> () as Transform;
		anim = GetComponent<Animator> ();	
	}

	void FixedUpdate() 
	{
		if (!stop){
			horiz = Input.GetAxis ("Horizontal");
			vert = Input.GetAxis ("Vertical");
			Move(horiz,vert,speed,vertCoef);
		}

	}
	
	// Update is called once peappears to be similar to pausing and unpausing the game. You can extract the relevant code from the community wiki's PauseMenu script, and I will be reprinting the relevant code here with an explanation:r frame
	void Update () 
	{
	}

	void Move (float _h, float _v, float _speed,float _vertCoef)
	{
			tr.position = tr.position +
			_h * transform.right
			* _speed * Time.fixedDeltaTime +
			_v * transform.forward
			* _speed * Time.fixedDeltaTime * _vertCoef;

		//animation
		Animating(_h,_v);
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

	public static void stopPlayer()
	{
		stop = true;
	}
	public static void unstopPlayer()
	{
		stop = false;
	}
}
