// Special thanks to Lily, who changed the perspective into player's and made this game much better
// Special thanks to Bennett and Wyett for code help
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// if we want to use the UI system, we need to say at the beginning of the script

public class GameLogic : MonoBehaviour {
	
	public int followerAmount = 40;
	public float cubeDistMin = 10.0f;
	public float cubeDistMax = 14.0f;
	public float followerDist = 3.0f;
	public float colorChangeRate = 0.01f;
	public float whiteAlpha;
	public float blackAlpha;
	
	public GameObject follower;
	public GameObject player;
	public GameObject cube;
	public GameObject followerPrefab;
	public GameObject white;
	public GameObject black; 

	public GameObject whiteScreen;

	public Color winColor;
	public Color loseColor;
	public Color grayText;
	public Color lastEndColor; 
	
	public bool hasPlayedSound = false;
	public bool endGameReached = false;
	public bool RestartAllowed = false;
	public bool toReset = false;


	int i = 1;

	// Use this for initialization
	void Start () 
	{	
	// make WhiteCube 
		player = GameObject.Find("Player");
		cube = Instantiate (Resources.Load("Cube")) as GameObject;
		makeCube ();

	// iniciating the win / lose scene
		// before game is won or lost, they are transparent
		winColor = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
		loseColor = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
		grayText = new Vector4 (0.5f, 0.5f, 0.5f, 0.0f);

		white = GameObject.Find("White");
		black = GameObject.Find("Black");

		//load the old colors
		whiteAlpha = PlayerPrefs.GetFloat ("WhiteAlpha");
		blackAlpha = PlayerPrefs.GetFloat ("BlackAlpha");

	}
	
	// Update is called once per frame
	void Update () 
	{
		fadeIn ();
		// make new followers
		if (i < followerAmount) 
		{
			makeFollower();
		}
		
		bool playerMoved = false;
		
		if (Input.anyKeyDown) 
		{
			playerMoved = true;
		}
	}

	void makeCube() 
	{ 
		//random range is 0-11 on z and 0-23 on x
		Vector3 rndCubePos = new Vector3 (Random.Range(1.0f, 22.0f), 3.0f, Random.Range(1.0f, 11.0f));
		float dist = Vector3.Distance(player.transform.position, rndCubePos);
		
		if (dist > cubeDistMin && dist < cubeDistMax) 
		{
			cube.transform.position = rndCubePos;
			cube.name = "WhiteCube";
		} 
		else 
		{
			makeCube();
		}
	}

	void fadeIn()
	{

		if (whiteAlpha > 0) {
			Image whiteImage = whiteScreen.GetComponent<Image> ();
			Color currentColor = whiteImage.color;
			whiteAlpha -= 0.016f;
			if (whiteAlpha < 0) whiteAlpha = 0;

			currentColor.a = whiteAlpha;
			whiteImage.color = currentColor;
		}

		if (blackAlpha > 0) {

			Image blackImage = black.GetComponent<Image> ();
			Color currentColor = blackImage.color;
			blackAlpha -= 0.016f;
			if (blackAlpha < 0) blackAlpha = 0;
			
			currentColor.a = blackAlpha;
			blackImage.color = currentColor;
			//Debug.Log ("black alpha; " + blackAlpha);
		}
	}

	void makeFollower() 
	{ 
	    //random range is 0-11 on z and 0-23 on x
		Vector3 rndFollowerPos = new Vector3 (Random.Range(1.0f, 22.0f), 3.0f, Random.Range(1.0f, 10.0f));
		float dist = Vector3.Distance(player.transform.position, rndFollowerPos);

		if (dist > followerDist) 
		{
			follower = Instantiate (followerPrefab, rndFollowerPos, Quaternion.identity) as GameObject;
			i ++;
		}
	}

	public void Win()
	{

		// WhiteCube glow when winning
		Light cubeLight = GameObject.Find ("WhiteCube").GetComponent<Light> ();
		cubeLight.range += 0.1f;

		// Screen turn into white
		// I kept the transparency at 1 so when it changes white it will be visible soon
		// When the transparency reached 1
		// gameEndReached confirmed
		if (winColor.a < 1.01f) 
		{
			winColor.a += colorChangeRate;
		} 
		if (winColor.a >= 1f)
		{
			winColor.a = 1f;
			endGameReached = true;
		}
		
		Image whiteImage = whiteScreen.GetComponent<Image>();
		whiteImage.color = winColor;
		
		// allow player to enter a new game when the screen is generally black / white
		if (endGameReached == true) 
		{
			Text endText;
			endText = GameObject.Find ("EndText").GetComponent<Text>();
			grayText.a += colorChangeRate;
			endText.color = grayText;
			
			if (grayText.a >= 0.9f && toReset == false) 
			{
				toReset = true;
			}
		}
		
		// restart game when any key is pressed
		if (toReset == true && Input.anyKeyDown) 
		{
			PlayerPrefs.SetFloat ("BlackAlpha", 0);
			PlayerPrefs.SetFloat ("WhiteAlpha", 1f);
			Application.LoadLevel ("stage1");
		}
			
		// play winning sound, only once when called at the first time
		if (hasPlayedSound == false) 
		{ 
			GameObject.Find ("White").GetComponent<AudioSource> ().Play ();
			hasPlayedSound = true;
		}

		// slowly stop bgm
		GameObject.Find ("GameMaster").GetComponent<AudioSource> ().volume -= 0.05f;
	}

	public void Lose()
	{	
	
		// Screen turn into black
		// I kept the transparency at 1 so when it changes back it will be visible soon

			if (loseColor.a < 1.01f) 
			{
				loseColor.a += colorChangeRate;
			} 
			if (loseColor.a >= 1f)
			{
				loseColor.a = 1f;
				endGameReached = true;
			}

		Image blackImage = GameObject.Find ("Black").GetComponent<Image>();
		blackImage.color = loseColor;

		// allow player to enter a new game when the screen is generally black / white
		if (endGameReached == true) 
		{
			Text endText;
			endText = GameObject.Find ("EndText").GetComponent<Text>();
			grayText.a += colorChangeRate;
			endText.color = grayText;
			
			if (grayText.a >= 0.9f && toReset == false) 
			{
					toReset = true;
			}
		}

		// restart game when any key is pressed
		if (toReset == true && Input.anyKeyDown) 
		{
			// playerPrefs is a great place to save anything! 
			// Infomation can be saved in here and then read
			// A nice place to save high scores~
			PlayerPrefs.SetFloat ("BlackAlpha", 1f); 
			PlayerPrefs.SetFloat ("WhiteAlpha", 0);
			Application.LoadLevel ("stage1");
		}
			
		// play winning sound, only once when called at the first time
		if (hasPlayedSound == false)
		{ 
			GameObject.Find ("Black").GetComponent<AudioSource>().Play();
			hasPlayedSound = true;
		}
			
		// slowly stop bgm
		GameObject.Find ("GameMaster").GetComponent<AudioSource> ().volume -= 0.05f;
	}

// GetComponent Example
//		GameObject.Find ("playerShell").GetComponent<playerShellControl> ().followerTouched ();
}