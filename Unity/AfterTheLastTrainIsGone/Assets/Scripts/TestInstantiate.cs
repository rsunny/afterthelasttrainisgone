using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestInstantiate : MonoBehaviour {

	public GameObject m_textBox;
	public GameObject m_textBoxPrefab;
	public Text m_textToDisplay;
	Transform tr;


	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_textBox = Instantiate(m_textBoxPrefab, tr) as GameObject;
		m_textToDisplay = m_textBox.transform.Find("Text").text;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
