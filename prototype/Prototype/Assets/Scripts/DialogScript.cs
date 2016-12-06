using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class DialogScript : MonoBehaviour {



	public enum DialogType
	{
		OkDialog,
		YesNoDialog
	};
	//public Canvas canvas;
	public CanvasGroup dialogCanvasGroup;
	public GameObject okDialogObject;
	public GameObject yesNoDialogObject;

	public  Text[] dialogText;
	public  Text[] dialogTitle;
	public static DialogScript instance;

	public static bool showingDialog;



	public delegate void dialogAnswer();
	public dialogAnswer okAnswer;
	public dialogAnswer noAnswer;

	public bool dialogResult = true;


	public static void PopUpDialog(string _title, string _text,DialogType _desiredDialog = DialogType.OkDialog, dialogAnswer _dialogAns = null, dialogAnswer _dialogNegativeAnswer = null)
	{	

		if (showingDialog)return;

		showingDialog = true;


		switch (_desiredDialog)
		{
			case DialogType.OkDialog:
				instance.okDialogObject.SetActive(true);
				instance.yesNoDialogObject.SetActive(false);
			break;
			case DialogType.YesNoDialog:
				instance.okDialogObject.SetActive(false);
				instance.yesNoDialogObject.SetActive(true);
			break;
		}


		for(int _i= 0; _i < instance.dialogText.Length; _i++)
		{
			instance.dialogText[_i].text = _text;
		}
			for(int _i= 0; _i < instance.dialogTitle.Length; _i++)
		{	
			instance.dialogTitle[_i].text = _title;
		}

		instance.dialogCanvasGroup.gameObject.SetActive(true);


		instance.okAnswer = _dialogAns;
		instance.noAnswer = _dialogNegativeAnswer;

	}

	public void DismissDialog(bool _answer)
	{	
		if (_answer)
		{
			if(okAnswer != null)
			{
				Debug.Log("sono qui");
				okAnswer();
			}
		}
		else
		{
			if (noAnswer != null)
			{
				noAnswer();
			}
		}
		
		//instance.dialogCanvasGroup.GetComponent<CanvasRenderer>().SetAlpha(0f);
        
		okDialogObject.SetActive(false);
        yesNoDialogObject.SetActive(false);
		
		dialogCanvasGroup.gameObject.SetActive(false);
		//instance.dialogCanvasGroup.DOFade(0, 0.5f).OnComplete(istance.hideCanvasGroup);
		// this.GetComponent<CanvasRenderer>().SetAlpha(0.1f);
 		//CanvasGroup.CrossFadeAlpha(0.1f,.1f,false);
 		//instance.dialogCanvasGroup.gameObject.SetAlpha(0.1f);
		//instance.dialogCanvasGroup.gameObject.SetAlpha(0.1f);
		//instance.dialogCanvasGroup.gameObject.SetActive(false);
		showingDialog =false;
	}

	public void hideCanvasGroup()
	{
		dialogCanvasGroup.gameObject.SetActive(false);
	}


	// Use this for initialization
	void Start () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
