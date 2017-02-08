using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTestScripts : MonoBehaviour {

	Vector3 posOri;
	Vector3 posAdjust;

	// Use this for initialization
	void Start () {
		posOri = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
//		Circle ();
//		Tan();
		EvenOdd();

		transform.position = posOri + posAdjust;
	}
		
	void Circle(){
		posAdjust = new Vector3 (0.0f, Mathf.Sin(Time.time),0.0f);
	}

	void Tan(){
		posAdjust = new Vector3 (0, Mathf.Tan(Time.time),0.0f);
	}

	void EvenOdd(){
		posAdjust = new Vector3 (
			0.0f, 
			Mathf.Tan(Time.time)* Mathf.Pow(-1, Mathf.RoundToInt(transform.position.x+transform.position.z)),
			0.0f);
	}
}
