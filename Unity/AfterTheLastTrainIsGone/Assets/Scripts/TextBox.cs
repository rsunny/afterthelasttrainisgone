using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

	public GameObject m_textBoxPrefab;
	public GameObject m_textBox;
	public Text m_textToDisplay;
	public TextAsset m_textFile;

	[Range (0,10)]
	public int m_maxNumberOfLinesToShow;
	public int m_initialCurrentLine;

	string[] m_textLines;
	int m_nbLines;

	int m_currentLine;
	int m_numberOfLinesToShow;
	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;

		if (m_textFile != null) {
			m_textLines = m_textFile.text.Split ('\n');
		}
		if (m_nbLines == 0) {
			m_nbLines = m_textLines.Length;
		}
	}
		
	
	// Update is called once per frame
	void Update () {
	}


	// Method to be called by player at each interaction, initiate the textbox or display the next lines
	public void PlayerInteraction(){
		if (m_textBox == null) {
			InstantiateTextBox ();
		} else {
			Next ();
		}
	}

	//Instantiate a textbox
	public void InstantiateTextBox(){
		if (m_textBox == null) {
			tr = GetComponent<Transform> () as Transform;
			m_textBox = Instantiate (m_textBoxPrefab, tr) as GameObject; // instantiate a textbox prefab and assign it
			m_textBox.transform.position += tr.position; // offset the textbox to its parents position
			m_textToDisplay = m_textBox.transform.GetChild (0).GetComponent<Text> (); // assign the text component of the textbox

			m_currentLine = m_initialCurrentLine; // initiation of the counters
			m_numberOfLinesToShow = m_maxNumberOfLinesToShow;
			Next ();
		}
	}

	// Display the next lines wrt the number of lines to display 
	// Change the value of textToDisplay according to the values of endLine and numberOfLinesToShow
	// Destroy the textBox if there is no more lines to display
	void Next(){
		if (m_numberOfLinesToShow == 0) {
			DestroyTextBox ();
		} else {
			if (m_textToDisplay != null) {
				m_textToDisplay.text = " ";
				if (m_numberOfLinesToShow > 0) {
					m_textToDisplay.text = m_textLines [m_currentLine];
					for (int i = 1; i < m_numberOfLinesToShow; i++) {
						m_textToDisplay.text += System.Environment.NewLine + m_textLines [m_currentLine + i];
					}
					m_currentLine += m_numberOfLinesToShow;
					m_numberOfLinesToShow = Mathf.Min (m_numberOfLinesToShow, m_nbLines - m_currentLine);
				}
			}
		}
	}
		
	//Destroy the textBox element and its children
	public void DestroyTextBox(){
		Destroy (m_textBox);
	}





			


}