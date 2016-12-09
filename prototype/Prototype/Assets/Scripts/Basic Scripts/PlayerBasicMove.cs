using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMove : MonoBehaviour {

	//instance of player
	public static PlayerBasicMove Instance = null;
	//bool to make it stop
	public static bool stop = false;

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
