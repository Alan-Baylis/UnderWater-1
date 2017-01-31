using UnityEngine;
using System.Collections;

public class CoralControl : MonoBehaviour {

	//public 	Light playerLight;
	public GameObject player;
	float distToPlayer;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		distToPlayer = Vector3.Distance (player.transform.position, transform.position);
		if (distToPlayer <= 5.0f) {
			player.GetComponent<PlayerShellControl> ().speed = 7 * distToPlayer / 5;
		}
		//transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}
}
