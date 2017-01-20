using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	[Range(0f,10f)]
	public float smoothing = 2f;
	public float minhorizontal = 10f;
	public float maxhorizontal = 500f;
	public float mindepth = 10f;
	
	Vector3 offset;

	// Use this for initialization
	void Start () {
		//target = GameObject.Find("Player").transform;
		//offset viene stabilito all'inizio come distanza tra camera e player
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {

		//ad ogni aggiornamento la camera rimane alla stessa distanza (offset) dal player
		Vector3 targetCamPos = target.position + offset;

		Vector3 positionTemp = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		//transform.Rotate(0,20*Time.deltaTime,0);

		transform.position = new Vector3 (Mathf.Max(minhorizontal,Mathf.Min (positionTemp.x, maxhorizontal)), positionTemp.y, Mathf.Max (positionTemp.z, mindepth));

	}
}
