using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PredatorControl : MonoBehaviour {

	public GameObject player;
	//public GameObject follower;

	public float moveForce = 5f;
	public float followDistMax = 3.0f;
	public float followDistMin = 0.3f;

	Rigidbody rbFollower;
	bool minusAlready = false;
	bool isShaking = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("Player");
		//follower = gameObject;
		rbFollower = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		float distToPlayer;
		distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (distToPlayer < followDistMax) 
		{
				follow();
	     }

		if (minusAlready == false) 
		{
			if (distToPlayer < followDistMin) 
			{
				if (!isShaking) {
					StartCoroutine (ShakeRoutine ());
				}
				minusAlready = true;
				moveForce = 0.0f;
				GameObject.Find ("PlayerShell").GetComponent<PlayerShellControl> ().followerTouched ();
				// this way of get component is important! we use it to talk to component and function

				//make some bubbles when touched
				GetComponent<ParticleSystem> ().Play(); 
			} 
		}
		if(minusAlready == true && gameObject != null)
		{
			// meanwhile size down
			transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
			
			// when scale reach 0,detory the follower:
			if(transform.localScale.x <= 0.0f)
			{
				Destroy(gameObject);
			}
		}
	}

	void follow()
	{
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		directionToPlayer.y = 0;
		rbFollower.AddForce(directionToPlayer * moveForce);
	}	

	IEnumerator ShakeRoutine(){
		isShaking = true;
		ShakeCamera sc= GameObject.Find ("GameMaster").GetComponent<ShakeCamera> ();
		sc.enabled = true;
		yield return new WaitForSeconds (1f);
		sc.enabled = false;
		isShaking = false;
	}

}
