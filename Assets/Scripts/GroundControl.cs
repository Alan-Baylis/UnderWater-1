using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {

	public float delayAdjustX = 0.2f;
	public float delayAdjustZ = 0.2f;

	private Vector3 height;

	// Use this for initialization
	void Start () {
		height = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float waveX = transform.position.x * delayAdjustX;
		float waveZ = transform.position.z * delayAdjustZ;

		Vector3 heightAdjust = new Vector3 (0.0f, Mathf.Sin (Time.time - waveX - waveZ), 0.0f);
		transform.position = height + heightAdjust;
	}
}
