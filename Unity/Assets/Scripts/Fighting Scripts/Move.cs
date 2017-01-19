using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public AudioClip m_stepSound;
	public AudioSource m_audioSource;
	private bool m_stepSoundActivated = false;
	private bool m_switchSound = false;

	//instance of player
	public static Move Instance = null;
	//bool to make it stop
	public static bool stop = false;

	//animarion
	public Animator anim;

	//trasform
	Transform tr;

	float horiz = 0f;
	float vert = 0f;
	[Range(0f,25f)]
	public float speed = 4f;
	[Range(0f,4f)]
	public float vertCoef = 1f;
	[Range(0f,4f)]
	public float m_runMultiplicator = 2f;

	private float m_currentSpeed;


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
		m_currentSpeed = speed;
	}

	void FixedUpdate() 
	{
		if (!stop){
			MoveFunction (horiz, vert);
		}

	}

	// Update is called once peappears to be similar to pausing and unpausing the game. You can extract the relevant code from the community wiki's PauseMenu script, and I will be reprinting the relevant code here with an explanation:r frame
	void Update () 
	{
		if (m_switchSound) {
			if (m_stepSoundActivated) {
				m_audioSource.Stop ();
				m_stepSoundActivated = false;
			} else {
				m_audioSource.PlayOneShot (m_stepSound);
				m_stepSoundActivated = true;
			}
			m_switchSound = false;
		}
	}

	public void Input (float horizontal, float depth){
		horiz = horizontal;
		vert = depth;
	}

	public void Run (bool run){
		if ( run)
		{
			m_currentSpeed = speed * m_runMultiplicator;
		}
		else 
		{
			m_currentSpeed = speed;
		}
	}

	private void MoveFunction (float _h, float _v)
	{
		float div = 1;
		if (_h != 0 || _v != 0) {
			div = (Mathf.Sqrt (_h * _h + _v * _v));
		}
			tr.position = tr.position +
				_h/div * transform.right * m_currentSpeed * Time.fixedDeltaTime +
				_v/div * transform.forward * m_currentSpeed * Time.fixedDeltaTime * vertCoef;

		//animation
		Animating(_h,_v);

		m_switchSound = (((_h != 0 || _v != 0) && !m_stepSoundActivated) || (_h==0 && _v==0 && m_stepSoundActivated));

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
		anim.SetBool("WalkingR", walkingR);


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
