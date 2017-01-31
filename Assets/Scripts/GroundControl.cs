using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {

	public float delayAdjustX;
	public float delayAdjustZ;

	private Vector3 height;
	private Rigidbody rb;

	//Rigidbody highWaveMakerRb;

	public bool touchingHighWaveMaker = false;

	Vector3 heightAdjust;

	// Use this for initialization
	void Start () {
		height = transform.position;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

//		if (this.transform.parent.GetComponent<GroundGroupHighWaveControl>().highwaving == true){
//			heightAdjust = new Vector3 (
//				0.0f, 
//				2f * Mathf.Sin (Time.time - transform.position.x * delayAdjustX - transform.position.z * delayAdjustZ), 
//				0.0f);
//		}else{
//

//		if(touchingHighWaveMaker == true){
//			heightAdjust = new Vector3 (
//				0.0f, 
//				2*Mathf.Sin (Time.time - transform.position.x * delayAdjustX - transform.position.z * delayAdjustZ), 
//				0.0f);
//		}else{
			
			heightAdjust = new Vector3 (
				0.0f, 
				Mathf.Sin (Time.time - transform.position.x * delayAdjustX - transform.position.z * delayAdjustZ), 
				0.0f);
//		}

//		}
		transform.position = height + heightAdjust;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "HighWaveMaker") {
			touchingHighWaveMaker = true;
		} else {
			touchingHighWaveMaker = false;
		}

		Debug.Log (touchingHighWaveMaker);
	}
}
