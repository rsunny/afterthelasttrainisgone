using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour {

	//SOUND
	public AudioClip m_trainSound;
	public AudioSource m_audioSource;

	public GameObject train;
	public float trainSpeed = 0.1f;
	public float trainAcceleration = 0.1f;
	private bool trainStart = false;

	// Use this for initialization
	void Start () 
	{
		train = GameObject.Find("Train");
	}

	void OnTriggerEnter (Collider collideObj)
	{
		if (collideObj.tag == "Player")
		{
			trainStart = true;
			if (m_audioSource != null) {
				m_audioSource.PlayOneShot (m_trainSound);
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(trainStart == true)
		{
			trainSpeed = trainSpeed + (Time.deltaTime * trainAcceleration);
			train.transform.Translate( Vector3.left * trainSpeed);
		}
	}
}
