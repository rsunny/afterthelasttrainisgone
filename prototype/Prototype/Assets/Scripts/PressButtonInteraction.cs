using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;


public class PressButtonInteraction : MonoBehaviour 
{
	// Singleton reference
    private static PressButtonInteraction _instance;

    // Public getter for the singleton reference
    public static PressButtonInteraction instance
    {
        get
        {
            // If there is no instance for PressButtonInteraction yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<PressButtonInteraction>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("PressButtonInteraction"));
                    singleton.name = "PressButtonInteraction";
                    _instance = singleton.GetComponent<PressButtonInteraction>();
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

	//canvas Group che contiene cosa mostrare
    public CanvasGroup dialogCanvasGroup;
	//collegamento al game object che useremo per pressButton
	public GameObject PressButton;

	//booleano che avvisa se del testo viene mostrato
	public static bool showingText;

	
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

	
	public static void PopUpText()
	{
        //If we're showing dialog already stop here.
        if (showingText) return;

		instance.PressButton.SetActive(true);
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingText = true;
		instance.dialogCanvasGroup.gameObject.SetActive(true);
	}

    public static void DismissText()
    {

        //Hide the gameobjects and set the showingText back to false to allow for new dialog calls.

		instance.PressButton.SetActive(false);
        instance.dialogCanvasGroup.gameObject.SetActive(false);
        showingText = false;
    }

}
