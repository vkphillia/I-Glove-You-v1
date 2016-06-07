using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public delegate void GloveEvent ();
public enum GameState
{
	RoundStart,
	Fight,
	Playing,
	Paused,
	RoundOver,
	MatchOver}
;

public class OfflineManager : MonoBehaviour
{
	public static event GloveEvent SpwanFirstGlove;
	
	//Static Singleton Instance
	public static OfflineManager _Instance = null;

	//property to get instance
	public static OfflineManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{                                                                                   
				_Instance = (OfflineManager)FindObjectOfType (typeof(OfflineManager));
			}
			return _Instance;
		}	
	}

	public bool Mute;
	//public AudioSource source_Punch;
	//public AudioSource source_RoundStart;
	//public AudioSource source_Fight;
	//public AudioSource source_Round;
	//public AudioSource[] source_RoundNumber;
	
	//scripts link
	public PlayerHolderController PlayerHolder1;
	public PlayerHolderController PlayerHolder2;
	public OfflineRoundController RoundPanel;



	public GameObject Player1HUDPanel;
	public GameObject Player2HUDPanel;

	//public Transform foreground;

	public GameState currentState;
    
	public bool glovePicked;
	public bool PUPicked;

	public int roundNumber;
	public int MaxHealth;
	public float MaxSpeed;
    
	//public float MaxRoundTimer;

	//public Text roundText_HUD;
	//public Text timerText_HUD;
	
	private Vector3 P1StartPos;
	private Vector3 P2StartPos;
	//private float roundTimer;

	public bool Pause;
	public Text PauseText;

	public P1Trophy TrophyP1;
	public P2Trophy TrophyP2;

	public PUController myPUController;




	public GameObject pauseBtn;

	//for placement
	[HideInInspector]
	public Vector3 screenSizeInWord;
	public Transform leftBorder;
	public Transform rightBorder;
	public Transform topBorder;
	public Transform botBorder;



	//for testing only
	public bool test_speedChange;

	void Awake ()
	{
		Blast.OnHit += makePlayerFall;
		WalkingBombBlastCol.OnHit += makePlayerFall;
	}

	//sets GameState to RoundStart and sets the sprite for both player
	void OnEnable ()
	{
		currentState = GameState.RoundStart;
		//
		PlayerHolder1.GetComponent<SpriteRenderer> ().sprite = PlayerHolder1.mySprites [OfflineMenuController.Player1CharacterID];
		PlayerHolder1.GetComponent<Animator> ().runtimeAnimatorController = PlayerHolder1.animationController [OfflineMenuController.Player1CharacterID];
		//Debug.Log (OfflineMenuController.Player1CharacterID);
		PlayerHolder2.GetComponent<SpriteRenderer> ().sprite = PlayerHolder2.mySprites [OfflineMenuController.Player2CharacterID];
		PlayerHolder2.GetComponent<Animator> ().runtimeAnimatorController = PlayerHolder2.animationController [OfflineMenuController.Player2CharacterID];
		//Debug.Log (OfflineMenuController.Player2CharacterID);
	}



	//sets the player intital position and calls ShowRoundPanel()
	void Start ()
	{
		
		screenSizeInWord = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		leftBorder.position = new Vector3 (-screenSizeInWord.x + .1f, 0, 0);
		rightBorder.position = new Vector3 (screenSizeInWord.x - .1f, 0, 0);
		topBorder.localScale = new Vector3 (screenSizeInWord.x - .2f, topBorder.localScale.y, topBorder.localScale.y);
		botBorder.localScale = new Vector3 (screenSizeInWord.x - .2f, botBorder.localScale.y, botBorder.localScale.y);


		//really need it? never used anywhere else
		/*P1StartPos = new Vector3 (0, -3, 0);
		P2StartPos = new Vector3 (0, 3, 0);
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;*/

		//foreground.transform.localScale = new Vector3 (.8f, 0.8f, 1);
        
		//RoundPanel.gameObject.SetActive(true);
		RoundPanel.ShowRoundPanel ();


        
	}
    
	//check for escape button click, spawn gloves and power ups, controls timer, checks round status
	void Update ()
	{

		//commenting this coz it gets too frustrating while playing
		/*if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (!Pause)
			{
				Pause = true;
				Time.timeScale = 0;

				PauseText.text = "Paused";
			}
			else
			{
				Pause = false;
				Time.timeScale = 1;
				PauseText.text = "";
			}
		}*/
		
		if (currentState == GameState.Fight)
		{
			ZoomIn ();
		}
		else if (currentState == GameState.RoundOver || currentState == GameState.MatchOver)
		{
			
			ZoomOut ();
			//myPUController.glove.gameObject.SetActive (false);
			myPUController.PU.GetComponent<PowerUp> ().DeactivatePU ();
			PUPicked = true;

		}
	}

	//camera zoom code
	void ZoomIn ()
	{
		Player1HUDPanel.SetActive (true);
		Player2HUDPanel.SetActive (true);

		/*if (foreground.transform.localScale.x < 1)
		{
			foreground.transform.localScale += new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}*/
	}

	public void ZoomOut ()
	{

		//Player1HUDPanel.SetActive (false);
		//Player2HUDPanel.SetActive (false);

		/*if (foreground.transform.localScale.x > 0.8f)
		{
			foreground.transform.localScale -= new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}*/
	}

	//checks for the winner and sets the GameState to MatchOver or RoundOver
	//any call for stoping the game should be sent here
	public void CheckRoundStatus ()
	{
		StartCoroutine (RoundStatusCoroutine ());
	}

	IEnumerator RoundStatusCoroutine ()
	{
		Time.timeScale = 0.2f;
		if (PlayerHolder1.myHealth > PlayerHolder2.myHealth)
		{
			PlayerHolder1.roundWins++;
			PlayerHolder2.myWalkAnim.Play ("Dead");
			TrophyP1.gameObject.SetActive (true);
		}
		else if (PlayerHolder2.myHealth > PlayerHolder1.myHealth)
		{
			PlayerHolder2.roundWins++;
			PlayerHolder1.myWalkAnim.Play ("Dead");
			TrophyP2.gameObject.SetActive (true);
		}

		
		if (PlayerHolder1.roundWins == 2 || PlayerHolder2.roundWins == 2)
		{
			currentState = GameState.MatchOver;
		}
		else
		{
			if (currentState != GameState.MatchOver)
			{
				currentState = GameState.RoundOver;
			}
		}
		yield return new WaitForSeconds (.3f);
		Time.timeScale = 1f;
		
		RoundPanel.ShowRoundPanel ();
	}

	//sets the players intital positions, timer and calls for SpawnGlove()
	public void StartNewRound ()
	{
		//roundTimer = MaxRoundTimer;
		//code for timer
		//GetComponentInChildren<ProgressBar> ().SetUpdateBar ((int)roundTimer);
		roundNumber++;
		PlayerHolder1.transform.localPosition = new Vector3 (-1.5f, -2.5f, 0);
		PlayerHolder1.transform.rotation = Quaternion.Euler (0, 0, -35);
		//PlayerHolder1.transform.rotation = Quaternion.identity;
		PlayerHolder1.ResetPlayer ();

		PlayerHolder2.transform.localPosition = new Vector3 (1.5f, 2.5f, 0);
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 155);
		PlayerHolder2.ResetPlayer ();

		//roundText_HUD.text = "Round: " + OfflineManager.Instance.roundNumber;


		//till here

		if (SpwanFirstGlove != null)
		{
			SpwanFirstGlove ();
		}


	}
    
	//sets the roundWins to 0 for both player
	public void NewMatchStart ()
	{
		PlayerHolder1.roundWins = 0;
		PlayerHolder2.roundWins = 0;
	}

	public void OnMenuClick ()
	{
		SceneManager.LoadScene ("offline menu");
	}



	//plays the sound that is passed in as an argument //Deprecated
	//public void PlaySound (AudioSource a)
	//{
	//	if (!Mute)
	//	{
	//		a.Play ();
	//	}
	//}


	public void OnPauseBtn ()
	{
		if (!Pause)
		{
			Pause = true;

			Time.timeScale = 0;

			PauseText.text = "Paused";
		}
		else
		{
			Pause = false;
			Time.timeScale = 1;
			PauseText.text = "II";
		}
	}

	void makePlayerFall (PlayerHolderController p)
	{
		StartCoroutine (p.MakeLyingDeadFalse ());
	}

	void OnDestroy ()
	{
		Blast.OnHit -= makePlayerFall;
		WalkingBombBlastCol.OnHit -= makePlayerFall;

	}
}
