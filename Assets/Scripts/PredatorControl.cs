using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PredatorControl : MonoBehaviour {

	public GameObject player;
	public GameObject gameMessager;

	public float predatorMF;
	public float attackDist;
	public float escapeDist;
	float distToPlayer;

	Rigidbody rbPredator;
	bool minusAlready = false;
	bool isShaking = false;
	bool attacking = false;

	public Material predatorMat;

	// Use this for initialization
	void Start (){
		player = GameObject.Find ("PlayerShell");
		gameMessager = GameObject.Find ("GameMessager");
		rbPredator = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		distToPlayer = Vector3.Distance(player.transform.position, transform.position);

		Debug.Log (distToPlayer);

		if (distToPlayer > attackDist && attacking == false){
			Idle ();
		}

		if (distToPlayer < attackDist){
			Attack();
			attacking = true;
	     }

		if (distToPlayer > escapeDist && attacking == true){
			StopAttack ();
			attacking = false;
		}
	}

	// fake as exist when player is far
	void Idle(){
		transform.Rotate (new Vector3 (45, 15, 30) * Time.deltaTime);
	}

	// attack when player is close, change apperance (un-recoverable)
	void Attack(){
		Debug.Log ("Attacking");

		// move to player
		transform.Rotate (new Vector3 (45, 15, 30) * 5 * Time.deltaTime);
		Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
		directionToPlayer.y = 0;
		rbPredator.AddForce(directionToPlayer * predatorMF);
		predatorMat.color = new Vector4 (1,0,0,1);
	}	

	// stop attack when player escape (far enough)
	void StopAttack(){
		Debug.Log ("Escaped");
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
