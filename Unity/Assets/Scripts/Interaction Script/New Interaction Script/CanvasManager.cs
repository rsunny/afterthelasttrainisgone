using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
	/*Introduction and initialization of the instance*/
	private static CanvasManager _instance;
		public static CanvasManager instance
		{
				get
				{
						// If there is no instance for CanvasManager yet and we're not shutting down at the moment
						if (_instance == null && !isShuttingDown)
						{
								//Try finding and instance in the scene
								_instance = GameObject.FindObjectOfType<CanvasManager>();
								//If no instance was found, let's create one
								if (!_instance)
								{
										GameObject singleton = (GameObject)Instantiate(Resources.Load("CanvasManager"));
										singleton.name = "CanvasManager";
										_instance = singleton.GetComponent<CanvasManager>();
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
	//end of Introduction



	/* A GROUP OF STATIC FUNCTION TO DISPLAY TEXT ON CANVAS
	-need to test it
	-need to add the default cases


	This class requires some elements of the canvas
	three basic canvas group:
	1.Tought with a text space and a image space
	2.dialog with two text space
	3.button with one text space


	*/


	public static int defaultTime = 1;

	public static bool showingText;

	/*Basic Variables*/
	public CanvasGroup ThoughtCanvasGroup;
	public GameObject ThoughtObject;
	public Text Thought;


	public CanvasGroup ImageCanvasGroup;
	//still need to inizialize 
	public GameObject ImageObject;
	public Image Image;
	//for the timed Tought

	public CanvasGroup DialogCanvasGroup;
	public GameObject DialogObject;
	public Text WhoIsSpeaking;
	public Text Dialog;

	public CanvasGroup ButtonCanvasGroup;
	public GameObject ButtonObject;
	public Text Button;


	public CanvasGroup StateChangeCanvasGroup;
	//public CanvasGroup StateImageCanvasGroup;
	public GameObject StateChangeObject;
	public GameObject StateImageObject;
	public Text StateSentence;
	public Image StateImage;




public static void ShowThought(string _thought = null, Sprite _thoughtImage = null)
{
	//check if there are preavious Tought or Dialog code
	if(showingText)return;
	//dismiss Previous Text
	DismissAll();
	//existence check
	if(_thought == null) _thought = "...";
	//start the execution
	showingText = true;
	//activate the Thought game object
	instance.ThoughtObject.SetActive(true);

	//instanciate the desired text
	instance.Thought.text = _thought;
	//check if an image exists
	if(_thoughtImage !=  null)
	{
		//instanciate the diesered image
		instance.Image.sprite = _thoughtImage;
		instance.Image.preserveAspect = true;
		instance.ImageObject.SetActive(true);
		instance.ImageCanvasGroup.gameObject.SetActive(true);
	}
	//activate the canvas Group
	instance.ThoughtCanvasGroup.gameObject.SetActive(true);
}

public static void DismissThought()
{
	instance.ThoughtObject.SetActive(false);
	instance.ThoughtCanvasGroup.gameObject.SetActive(false);
	instance.ImageObject.SetActive(false);
	instance.ImageCanvasGroup.gameObject.SetActive(false);
	showingText = false;

}

public static void ShowTimedTought(string _thought, Sprite _thoughtImage = null, int _time = 1)
{
	ShowThought(_thought, _thoughtImage);
	defaultTime = _time;
	instance.StartCoroutine(waitAndDestroy());
}

private static IEnumerator waitAndDestroy()
{
	yield return new WaitForSeconds(defaultTime);
	DismissThought();
}


public static void ShowDialog(string _whoIsSpeaking = null, string _dialog = null)
{
	//check if there are preavious Tought or Dialog code
	if(showingText)return;
	//dismiss Previous Text
	DismissAll();
	//existence check
	if(_whoIsSpeaking == null) _whoIsSpeaking =  "";
	if(_dialog == null) _dialog = "...";
	//start the execution
	showingText = true;
	//activate the Dialog game object
	instance.DialogObject.SetActive(true);
	//instanciate the desired text
	instance.WhoIsSpeaking.text = _whoIsSpeaking;
	instance.Dialog.text = _dialog;
	//activate the canvas Group
	instance.DialogCanvasGroup.gameObject.SetActive(true);
}
public static void DismissDialog()
{
	instance.DialogObject.SetActive(false);
	instance.DialogCanvasGroup.gameObject.SetActive(false);
	showingText = false;

}

public static void ShowButton(string _button, bool _firsTime = false)
{
	//check if there are preavious Tought or Dialog code
	if(showingText)return;
	//dismiss Previous Text
	DismissAll();
	//existence check
	if(_button == null) return;
	//start the execution
	showingText = false;

	if(_firsTime)
	{
		instance.ButtonCanvasGroup.alpha = 0.9f ;
	}
	else
	{
		instance.ButtonCanvasGroup.alpha = 0.4f ;
	} 


	//activate the Dialog game object
	instance.ButtonObject.SetActive(true);
	//instanciate the desired text
	instance.Button.text = _button;
	//activate the canvas Group
	instance.ButtonCanvasGroup.gameObject.SetActive(true);
}
public static void DismissButton()
{
	instance.ButtonObject.SetActive(false);
	instance.ButtonCanvasGroup.gameObject.SetActive(false);
	showingText = false;

}


public static void DismissAll()
{
	DismissThought();
	DismissDialog();
	DismissButton();
}



public static void DismissChangeState()
{
	instance.StateChangeObject.SetActive(false);
	instance.StateChangeCanvasGroup.gameObject.SetActive(false);
	instance.StateImageObject.SetActive(false);
	//instance.StateImageCanvasGroup.gameObject.SetActive(false);
}


private static void ShowStateSetence(string _sentence = null, Sprite _stateImage = null)
{

	if(_sentence == null) return;
	//activate the Thought game object
	instance.StateChangeObject.SetActive(true);

	//instanciate the desired text
	instance.StateSentence.text = _sentence;
	//check if an image exists
	if(_stateImage !=  null)
	{
		//instanciate the diesered image
		instance.StateImage.sprite = _stateImage;
		instance.StateImage.preserveAspect = true;
		instance.StateImageObject.SetActive(true);
		//instance.StateImageCanvasGroup.gameObject.SetActive(true);
	}
	//else instance.StateImageObject.SetActive(false);
	//activate the canvas Group
	instance.StateChangeCanvasGroup.gameObject.SetActive(true);
}

public static void ShowChangeState(string _sentence = null, Sprite _image = null, int _time = 2)
{
	ShowStateSetence(_sentence, _image);
	int time = _time;
	instance.StartCoroutine(waitAndDestroyState(time));
}

private static IEnumerator waitAndDestroyState(int _t)
{
	yield return new WaitForSeconds(_t);
	DismissChangeState();
}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
