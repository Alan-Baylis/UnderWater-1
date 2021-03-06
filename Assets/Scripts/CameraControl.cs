﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

//	public float MoveSpeed = 10f;

	private Vector3 playerVelocity;
	
	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
		Debug.Log(offset);
//		playerVelocity = new Vector3 (0, 0, 10);
	}

//  // make camera rotate with player when they turn
//	void FixedUpdate(){
//		Rigidbody playerRB = player.transform.parent.gameObject.GetComponent<Rigidbody> ();
//		float blendAmount = 0.01f * Mathf.Clamp (playerRB.velocity.magnitude, 0.0f, 1.0f);
//		// velocity = Get the world-space speed
//		// magnitude = Returns the length of this vector
//
//		Vector3 rbVelocity = playerRB.velocity;
//		rbVelocity.y = 0; //because we don't want camera to go above player
//
//		playerVelocity = Vector3.Lerp (playerVelocity, rbVelocity, blendAmount); 
//		//that just makes a vector that is 90% playerVelocity and 10% playerRB.velocity
//		//so it slowly follows playerVelocity
//	}

	// late update is run after all update functions

	void Update(){
		//transform.position = player.transform.position + offset;

//		transform.position = Vector3.Lerp (transform.position, player.transform.position + offset, MoveSpeed * Time.deltaTime);
	}

	void LateUpdate () 
	{ 
		Vector3 curRotation = player.transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0, curRotation.y, 0);
		transform.LookAt(player.transform);
		RaycastHit hit;
		Ray shootingRay = new Ray(transform.position, Vector3.down);
		if(Physics.Raycast(shootingRay, out hit, 100)) {
			Vector3 verticalVector = new Vector3(transform.position.x, hit.point.y, transform.position.z);
			if(hit.distance > 2) {
			transform.position = Vector3.Lerp(transform.position, verticalVector, 0.005f);
			}
			else {
				transform.position = Vector3.LerpUnclamped(verticalVector, transform.position, 1.005f);
			}

		} 
		Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		if(Vector3.Distance(transform.position, newPos) > 2){
			transform.position = Vector3.Lerp(transform.position, newPos, 0.005f);
		}
		else if(Vector3.Distance(transform.position, newPos) < 1){
			transform.position = Vector3.LerpUnclamped(newPos, transform.position, 1.005f);
		}
		//transform.position = player.transform.position + offset;
////
  // camera rotating part 2, commend out the "+ offset" line and use below
//			+ (playerVelocity.normalized * -1f) + new Vector3(0,0.1f,0); 
			//move the camera behind the player
//		transform.LookAt (player.transform);
	}

}
