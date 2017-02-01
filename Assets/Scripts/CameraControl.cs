using UnityEngine;
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
		transform.position = player.transform.position + offset;
////
  // camera rotating part 2, commend out the "+ offset" line and use below
//			+ (playerVelocity.normalized * -1f) + new Vector3(0,0.1f,0); 
			//move the camera behind the player
//		transform.LookAt (player.transform);
	}
}
