using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject m_textBox;
	public Text m_textToDisplay;
	public TextAsset m_textFile;

	public string[] m_textLines;
	public int m_currentLine;
	public int m_length;

	[Range (0,10)]
	public int m_numberOfLinesToShow;


	// Use this for initialization
	void Start () {
		if (m_textFile != null) {
			m_textLines = m_textFile.text.Split ('\n');
		}
		if (m_length == 0) {
			m_length = m_textLines.Length;
		}
		Next ();
	}
		
	
	// Update is called once per frame
	void Update () {
			if (Input.GetKeyDown ("space")) {
				if (m_numberOfLinesToShow == 0) {
					DestroyTextBox ();
				} else {
					Next ();
				}
			}
	}


	//Change the value of textToDisplay according to the values of endLine and numberOfLinesToShow
	void Next(){
		m_textToDisplay.text = " ";
		if (m_numberOfLinesToShow>0) {
			m_textToDisplay.text = m_textLines [m_currentLine];
			for (int i = 1; i < m_numberOfLinesToShow; i++) {
				m_textToDisplay.text += System.Environment.NewLine + m_textLines [m_currentLine + i];
			}
			m_currentLine += m_numberOfLinesToShow;
			m_numberOfLinesToShow = Mathf.Min (m_numberOfLinesToShow, m_length-m_currentLine);

		}
	}


	//Destroy the textBox element and its children
	void DestroyTextBox(){
		Destroy (m_textBox);
	}


	//Instantiate a textbox
	void CreateTextBox(){
	}
}
