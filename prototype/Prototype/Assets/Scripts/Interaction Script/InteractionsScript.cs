using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionsScript : MonoBehaviour 
{
	private static InteractionsScript _instance;

    public static InteractionsScript instance
    {
        get
        {
            // If there is no instance for InteractionsScript yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<InteractionsScript>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("InteractionsScript"));
                    singleton.name = "InteractionsScript";
                    _instance = singleton.GetComponent<InteractionsScript>();
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

	
	
	public enum InteractionType
    {
        Thought, //OkDialog,
        Dialog //Dialog
    };
	//Store the container panel that coantains both dialog boxes.
    public CanvasGroup interactionCanvasGroup;
	//Game object for Thought
    public GameObject ThoughtObject;
    //Game Object for the Dialog
    public GameObject DialogObject;
	
	//Here go the name of the characther speaking (only Dialog) for both the game object
    public Text[] whoIsSpeaking;
    //Here go thoughts and dialogs for both the game objects
    public Text[] texts;

	/*DELEGATE FUNCTION: uses for change state of object after dialogs/interactions*/
	//We're going to use a void delegate for the callbacks
    public delegate void consequence();
    //We have one
    public consequence next;

	//Bool to check if there is already a dialog currently showing.
    public static bool showingInteraction;

	
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

    public static void PopUpInteraction(string _whoIsSpeaking, string _texts, InteractionType _desiredDialog = InteractionType.Thought, consequence _consequence = null)
    {
        //If we're showing dialog already stop here.
        if (showingInteraction) return;
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingInteraction = true;
        
        //Set our dialog boxes to show or not show based on it's desired type.
        switch (_desiredDialog)
        {
                case InteractionType.Thought:
                    Debug.Log("sto per attivare sto oggettiello");
                    Debug.Log(instance.ThoughtObject.activeSelf ? "Active" : "Inactive");
                    instance.ThoughtObject.SetActive(true);
                    Debug.Log(instance.ThoughtObject.activeSelf ? "Active" : "Inactive");
                    instance.DialogObject.SetActive(false);
                break;
                case InteractionType.Dialog:
                    instance.ThoughtObject.SetActive(false);
                    instance.DialogObject.SetActive(true);
                break;
        }

        //Fill all the texts with the desired text.
         for (int _i = 0; _i < instance.texts.Length; _i++)
        {
            instance.texts[_i].text = _texts;
        }

        //If it is a dialog there will be who is speaking
        if(_desiredDialog == InteractionType.Dialog)
        {
            for (int _i = 0; _i < instance.whoIsSpeaking.Length; _i++)
            {
                instance.whoIsSpeaking[_i].text = _whoIsSpeaking;
            }
        }

        //Show the dialog canvas.
        instance.interactionCanvasGroup.gameObject.SetActive(true);

        //Set our callbacks to the ones we received.
        instance.next = _consequence;
    }



    public static void DismissText()
    {
      // consequence();
        //Hide the gameobjects and set the showingInteraction back to false to allow for new dialog calls.
        instance.ThoughtObject.SetActive(false);
        instance.DialogObject.SetActive(false);
        instance.interactionCanvasGroup.gameObject.SetActive(false);
        showingInteraction = false;
    }
}
