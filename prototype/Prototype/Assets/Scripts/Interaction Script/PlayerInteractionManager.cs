using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour {

	private static PlayerInteractionManager _instance;
    public static PlayerInteractionManager instance
    {
        get
        {
            // If there is no instance for PlayerInteractionManager yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<PlayerInteractionManager>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("PlayerInteractionManager"));
                    singleton.name = "PlayerInteractionManager";
                    _instance = singleton.GetComponent<PlayerInteractionManager>();
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
