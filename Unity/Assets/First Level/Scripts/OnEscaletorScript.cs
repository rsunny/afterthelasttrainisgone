using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEscaletorScript : MonoBehaviour {

	public GameObject m_player;
	public bool goingDown = true;
	public float speedH = 1;
	public float speedV = 0;

	void OnTriggerEnter (Collider collideObj)
	{	
		if (collideObj.tag == "Player")
		{
			m_player.GetComponent<PlayerManager> ().DisableAction (true);
		}
	}

	void OnTriggerStay (Collider collideObj)
	{	
		if (collideObj.tag == "Player" )
		{			
			m_player.transform.Translate(Vector3.right * Time.deltaTime * speedH);
			m_player.transform.Translate(Vector3.up * Time.deltaTime * speedV);
		}
	}

	void OnTriggerExit (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			m_player.GetComponent<PlayerManager> ().DisableAction (false);
		}
	}
	// Use this for initialization
	void Start () 
	{
		m_player = GameObject.Find("Player");
		if(goingDown) speedV = - speedV;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
