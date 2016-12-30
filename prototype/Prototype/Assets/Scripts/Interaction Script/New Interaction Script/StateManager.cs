using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	/*Introduction and initialization of the instance*/
	private static StateManager _instance;
	public static StateManager instance
	{
		get
		{
			// If there is no instance for StateManager yet and we're not shutting down at the moment
			if (_instance == null && !isShuttingDown)
			{
				//Try finding and instance in the scene
				_instance = GameObject.FindObjectOfType<StateManager>();
				//If no instance was found, let's create one
				if (!_instance)
				{
					GameObject singleton = (GameObject)Instantiate(Resources.Load("StateManager"));
					singleton.name = "StateManager";
					_instance = singleton.GetComponent<StateManager>();
				}
				//Set the instance to persist between levels.
				DontDestroyOnLoad(_instance.gameObject);
			}
			//Return an instance, either that we found or that we created.
			return _instance;
		}
	}
	public static bool isShuttingDown;
	//Unity calls this function when quitting, I'm using that info to avoid creating
	//something when the game is quitting as unity doesn't like that.
	void OnApplicationQuit()
	{
		isShuttingDown = true;
	}
	void Awake()
    {
        //If there is no instance of this currently in the scene
        if (_instance == null)
        {
            //Set ourselves as the instance and mark us to persist between scenes
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If there is already an instance of this and It's not me, then destroy me as there should only be one.
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
	//not used yet
	public static int maxState = 2;

	public static int currentState = 1;
	public static void setCurrentState(int _i)
	{
		currentState = _i;
	}

	public static void updateState()
	{
		currentState++;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
