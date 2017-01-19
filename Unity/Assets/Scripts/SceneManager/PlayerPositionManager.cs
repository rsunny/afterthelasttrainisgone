using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour {

	public const string sceneKey = "scene";
	public string thisScene;
	
	public PlayerPositions[] StartingPositions;
	
	public GameObject player;
	public GameObject camera;
	
	//private Vector3 defaultPosition;
	
	[System.Serializable]
	public struct PlayerPositions
	{
		public string fromScene;
		public Vector3 position;
	}
	public Vector3 deltaCamera = new Vector3(0, 3, -18);
	private float _x;
	private float _y;
	private float _z;

	private float _deltax;
	private float _deltay;
	private float _deltaz;
	


	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player");
		camera = GameObject.Find("PlayerCamera");

		_deltax = deltaCamera.x;
		_deltay = deltaCamera.y;
		_deltaz = deltaCamera.z;

		string previousScene = ApplicationModel.GetString(sceneKey);

		for(int _i = 0; _i < StartingPositions.Length ; _i++)
		{

			if (previousScene == StartingPositions[_i].fromScene)
			{
				_x = StartingPositions[_i].position.x;
				_y = StartingPositions[_i].position.y;
				_z = StartingPositions[_i].position.z;
				player.transform.position = new Vector3( _x, _y, _z);
				camera.transform.position = new Vector3( _x + _deltax, _y + _deltay, _z + _deltaz);

			}

		}
		ApplicationModel.SetString( sceneKey,thisScene );

		

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
