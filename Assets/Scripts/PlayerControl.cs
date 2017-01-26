using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	float lightAmplitudeControl = 0.3f;
	float oriLtRange;
	Light playerLight;
	public GameObject playerShell;
	
	// Use this for initialization
	void Start () 
	{
		playerLight = GetComponent<Light>();
		oriLtRange = playerLight.range;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = playerShell.transform.position;

		// shooting a raycast to the direction player moving to 
		Debug.DrawRay(transform.position, playerShell.GetComponent<PlayerShellControl> ().movement, Color.green);

		float amplitude = (Mathf.Sin (2.0f * Time.time) + 1.0f) * lightAmplitudeControl;

		GameObject dest = GameObject.Find ("WhiteCube");

		Vector3 destPos = dest.transform.position;
		Vector3 startPos = new Vector3 (11.5f, 2.5f, 1.76f);
//		Vector3 centerPos = new Vector3 (11.5f, 0f, 5.5f);

		float distToDest = Vector3.Distance (transform.position, destPos);
		float oriDistToDest = Vector3.Distance (startPos, destPos);	
//		float distToCenter = Vector3.Distance (transform.position, centerPos);	
//		float oriDistToCenter = Vector3.Distance (startPos, centerPos);	
//
		playerLight.range = oriLtRange * (1f + 0.15f * amplitude + 0.07f * (oriDistToDest - distToDest));

//		//Example of distance
//		Vector3 rndCubePos = new Vector3 (Random.Range(1.0f, 22.0f), 3.0f, Random.Range(8.0f, 11.0f));
//		float dist = Vector3.Distance(transform.position, rndCubePos);
	}
}

