using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

	// On first player interaction, instantiates a textbox according to the textBoxPrefab attributes which is a canvas and a text
	// On every player interaction and also the first one, updates the content of the text to display according to the m_maxNumberOfLinesToShow and m_currentLine atributes
	// The textbox is destroyed when there is no more text to display or when 

	// Textbox prefab and text to display
	public GameObject m_textBoxPrefab;
	public TextAsset m_textFile;

	// Maximum lines to display at once and number of line to start with
	[Range (0,10)]
	public int m_maxNumberOfLinesToShow;
	public int m_initialCurrentLine;

	// Used to instantiate the prefab which is a canvas and a text
	GameObject m_textBox; 
	Text m_textToDisplay;

	string[] m_textLines; // Stores the lines to display in a list
	int m_nbLines; // Number of lines
	int m_currentLine; 
	int m_numberOfLinesToShow; // Number of lines to show next. Equals zero at the end

	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;

		// Splits the text in lines and stores it in a list
		if (m_textFile != null) {
			m_textLines = m_textFile.text.Split ('\n');
		}

		// Number of lines equals to the size of the previous list
		m_nbLines = m_textLines.Length;
	}
		
	
	// Update is called once per frame
	void Update () {
	}


	// Method to be called by player at each interaction, initiates the textbox or displays the next lines
	public void PlayerInteraction(){
		if (m_textBox == null) {
			InstantiateTextBox ();
		} else {
			NextLines ();
		}
	}

	// Instantiates the textbox
	// Sets m_textBox as the canvas and m_textToDisplay as the text
	// Displays the first lines calling the Next() function
	public void InstantiateTextBox(){
		if (m_textBox == null) {
			tr = GetComponent<Transform> () as Transform;
			m_textBox = Instantiate (m_textBoxPrefab, tr) as GameObject; // Instantiates a textbox prefab and assigns it as children of the current gameobject
			m_textBox.transform.position += tr.position; // Offsets the textbox to its parent position
			m_textToDisplay = m_textBox.transform.GetChild (0).GetComponent<Text> (); // Assign the text component of the textbox

			m_currentLine = m_initialCurrentLine; // Initiation of the line counter
			m_numberOfLinesToShow = m_maxNumberOfLinesToShow; // Initiation of the number of line to show
			NextLines (); // Call the next method to display the first lines
		}
	}

	// Display the next lines wrt the number of lines to display 
	// Change the value of textToDisplay according to the values of endLine and numberOfLinesToShow
	// Destroy the textBox if there is no more lines to display
	void NextLines(){
		if (m_numberOfLinesToShow == 0) {
			DestroyTextBox ();
		} else {
			if (m_textToDisplay != null) {
				m_textToDisplay.text = " "; //initiation of the text to display string
				if (m_numberOfLinesToShow > 0) {
					m_textToDisplay.text = m_textLines [m_currentLine]; //adds the current line to the display string
					for (int i = 1; i < m_numberOfLinesToShow; i++) { //according to the amount of lines to display, adds the following lines
						m_textToDisplay.text += System.Environment.NewLine + m_textLines [m_currentLine + i];
					}
					m_currentLine += m_numberOfLinesToShow; // updates the line counter
					// updates the number of the upcoming lines to show comparing the maximum number of lines to show and the number of the remaining ones
					m_numberOfLinesToShow = Mathf.Min (m_numberOfLinesToShow, m_nbLines - m_currentLine);
				}
			}
		}
	}
		
	//Destroy the textBox element and its children
	public void DestroyTextBox(){
		if (m_textBox != null) {
			Destroy (m_textBox);
		}
	}
}