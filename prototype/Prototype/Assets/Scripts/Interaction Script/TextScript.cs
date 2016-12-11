using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {
	
	
	private static TextScript _instance;
    public static TextScript instance
    {
        get
        {
            // If there is no instance for TextScript yet and we're not shutting down at the moment
            if (_instance == null && !isShuttingDown)
            {
                //Try finding and instance in the scene
                _instance = GameObject.FindObjectOfType<TextScript>();
                //If no instance was found, let's create one
                if (!_instance)
                {
                    GameObject singleton = (GameObject)Instantiate(Resources.Load("TextScript"));
                    singleton.name = "TextScript";
                    _instance = singleton.GetComponent<TextScript>();
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
	
	
	
	
	
	//Store the container panel that coantains both dialog boxes.
    public CanvasGroup ThoughtCanvasGroup;
	//Game object for Thought
    public GameObject ThoughtObject;
	//what is thinking
	public Text[] ThoughtText;

    
    //Store the container panel that coantains both dialog boxes.
    public CanvasGroup DialogCanvasGroup;
	//Game object for Thought
    public GameObject DialogObject;
	//who is speaking
    public Text[] WhoIsSpeaking;
    //what is syaing
    public Text[] DialogueText;

	//canvas Group che contiene cosa mostrare
    public CanvasGroup buttonCanvasGroup;
	//collegamento al game object che useremo per pressButton
	public GameObject PressButton;


	/*DELEGATE FUNCTION: uses for change state of object after dialogs/interactions*/
	//We're going to use a void delegate for the callbacks
    public delegate void consequence();
    //We have one
    public consequence next;
	//Bool to check if there is already a dialog currently showing.
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

	public static void PopUpThought (string _thougth, consequence _consequence = null)
	{
		//If we're showing dialog already stop here.
        if (showingText) return;
		DismissText();
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingText = true;    
		
        
		instance.ThoughtObject.SetActive(true);
		for(int _i= 0; _i < instance.ThoughtText.Length; _i++)
		{
			instance.ThoughtText[_i].text = _thougth;
		}
		
		//Show the dialog canvas.
        instance.ThoughtCanvasGroup.gameObject.SetActive(true);
		
		//Set our callbacks to the ones we received.
        if(_consequence == null)
        {
            return;
        } 
        else  instance.next = _consequence;
	}
    public static void PopUpDialog(string _whoIsSpeaking, string _whatHeSays, consequence _consequence = null)
    {
        //If we're showing dialog already stop here.
        if (showingText) return;
		DismissText();
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingText = true;    

        instance.DialogObject.SetActive(true);
        instance.DialogCanvasGroup.gameObject.SetActive(true);
		for(int _i= 0; _i < instance.WhoIsSpeaking.Length; _i++)
		{
			instance.WhoIsSpeaking[_i].text = _whoIsSpeaking;
		}
		for(int _i= 0; _i < instance.DialogueText.Length; _i++)
		{
			instance.DialogueText[_i].text = _whatHeSays;
		}
        //Show the dialog canvas.
		
		//Set our callbacks to the ones we received.
        if(_consequence == null)
        {
            return;
        } 
        else  instance.next = _consequence;
    }
	public static void PopUpPressButton()
	{
        //If we're showing dialog already stop here.
        if (showingText) return;

		DismissText();
		
		instance.PressButton.SetActive(true);
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingText = false;
		instance.buttonCanvasGroup.gameObject.SetActive(true);
	}

    public static void PopUpTimedThought(string _thougth)
    {   
        Debug.Log("sono dentro");
        //If we're showing dialog already stop here.
        if (showingText) return;
		DismissText();
        Debug.Log("ho superato il primo script e dismesso vecchio testo");
        //Set the showing dialog bool to true to prevent another dialog over this.
        showingText = true;    
		
        
		instance.ThoughtObject.SetActive(true);
		for(int _i= 0; _i < instance.ThoughtText.Length; _i++)
		{
            Debug.Log("sono dentro il ciclo for");
            Debug.Log(_thougth);
			instance.ThoughtText[_i].text = _thougth;
		}
		
		//Show the dialog canvas.
        instance.ThoughtCanvasGroup.gameObject.SetActive(true);
		
    }


	public static void DismissText()
    {
      // consequence();
        //Hide the gameobjects and set the showingText back to false to allow for new dialog calls.
     	instance.PressButton.SetActive(false);
		instance.buttonCanvasGroup.gameObject.SetActive(false);
	    instance.ThoughtObject.SetActive(false);
        instance.ThoughtCanvasGroup.gameObject.SetActive(false);
	    instance.DialogObject.SetActive(false);
        instance.DialogCanvasGroup.gameObject.SetActive(false);        
        showingText = false;
    }

}
