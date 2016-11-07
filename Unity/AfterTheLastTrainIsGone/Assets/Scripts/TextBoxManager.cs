using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;
	public TextAsset textFile;

	public Text textToDisplay;

	public string[] textLines;

	public int currentLine;

	public int endLine;

	[Range (0,10)]
	public int numberOfLinesToShow;


	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = textFile.text.Split ('\n');
		}

		if (endLine == 0) {
			endLine = textLines.Length - 1;
		}

		Next ();


	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			Next ();
		}
	}

	void Next(){
		textToDisplay.text = "";
		if (currentLine <= endLine) {
			textToDisplay.text = textLines [currentLine];
			for (int i = 1; i < numberOfLinesToShow; i++) {
				textToDisplay.text += System.Environment.NewLine + textLines [currentLine + i];
			}
			currentLine += numberOfLinesToShow;
			numberOfLinesToShow = Mathf.Min (numberOfLinesToShow, endLine-currentLine);

		}
		
	}
}
