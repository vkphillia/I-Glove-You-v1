using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public GameObject glove;

	public GameObject Player1HUDPanel;
	public GameObject Player2HUDPanel;

	public Transform foreground;

	public GameState currentState;

	public bool glovePicked;
	public bool PUPicked;

	public int roundNumber;
	public int MaxHealth;
	public float MaxSpeed;
    
	public float MaxRoundTimer;

	public Text roundText_HUD;
	public Text timerText_HUD;
	
	private Vector3 P1StartPos;
	private Vector3 P2StartPos;
	private float roundTimer;

	//sets GameState to RoundStart and sets the sprite for both player
	void OnEnable ()
	{
		currentState = GameState.RoundStart;
		//why we need this when we know that there are 2 players and we have 2 sprites
		PlayerHolder1.GetComponent<SpriteRenderer> ().sprite = PlayerHolder1.mySprites [0];
		PlayerHolder2.GetComponent<SpriteRenderer> ().sprite = PlayerHolder2.mySprites [1];
	}

	//sets the player intital position and calls ShowRoundPanel()
	void Start ()
	{
		P1StartPos = new Vector3 (0, -3, 0);
		P2StartPos = new Vector3 (0, 3, 0);
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;
		foreground.transform.localScale = new Vector3 (.8f, 0.8f, 1);

		ShowRoundPanel ();


	}

	//sets round start and over texts
	public void ShowRoundPanel ()
	{
		RoundPanel.gameObject.SetActive (true);

		if (currentState == GameState.RoundStart)
		{
			StartCoroutine (RoundPanel.HideRoundStartText ());
		}
		else if (currentState == GameState.RoundOver)
		{
			StartCoroutine (RoundPanel.HideRoundOverText ());
		}
		else if (currentState == GameState.MatchOver)
		{
			StartCoroutine (RoundPanel.HideMatchOverText ());
		}
	}

	//check for escape button click, spawn gloves and power ups, controls timer, checks round status
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			SceneManager.LoadScene ("offline menu");
		}

		if (currentState == GameState.Playing)
		{
			if (glovePicked)
			{
				StartCoroutine (SpawnGloveCoroutine ());
			}


			//Timer controller
			roundTimer -= Time.deltaTime;
			timerText_HUD.text = "Time: " + roundTimer.ToString ("N0");

			if (roundTimer <= 0)
			{
				//Times up and round is over
				CheckRoundStatus ();
			}
		}
		else if (currentState == GameState.Fight)
		{
			ZoomIn ();

			//Debug.Log (PlayerHolder1.transform.position);
			//Debug.Log (PlayerHolder2.transform.position);
		}
		else if (currentState == GameState.RoundOver || currentState == GameState.MatchOver)
		{
			ZoomOut ();
			StopCoroutine (SpawnGloveCoroutine ());
		}
	}

	//spawn gloves code
	public IEnumerator SpawnGloveCoroutine ()
	{
		glovePicked = false;
		yield return new WaitForSeconds (7f);
		SpawnGlove ();
	}

	void SpawnGlove ()
	{
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0);
	}



	//camera zoom code
	void ZoomIn ()
	{
		Player1HUDPanel.SetActive (true);
		Player2HUDPanel.SetActive (true);

		if (foreground.transform.localScale.x < 1)
		{
			foreground.transform.localScale += new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	public void ZoomOut ()
	{
		Player1HUDPanel.SetActive (false);
		Player2HUDPanel.SetActive (false);

		if (foreground.transform.localScale.x > 0.8f)
		{
			foreground.transform.localScale -= new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	//checks for the winner and sets the GameState to MatchOver or RoundOver
	void CheckRoundStatus ()
	{
		if (PlayerHolder1.myHealth > PlayerHolder2.myHealth && PlayerHolder1.roundWins == 1)
		{
			PlayerHolder1.roundWins++;
			currentState = GameState.MatchOver;
		}
		else if (PlayerHolder2.myHealth > PlayerHolder1.myHealth && PlayerHolder2.roundWins == 2)
		{
			PlayerHolder2.roundWins++;
			currentState = GameState.MatchOver;
		}
		else
		{
			currentState = GameState.RoundOver;
		}

		ShowRoundPanel ();
	}

	//sets the players intital positions, timer and calls for SpawnGlove()
	public void StartNewRound ()
	{
		roundTimer = MaxRoundTimer;
		roundNumber++;

		PlayerHolder1.transform.localPosition = new Vector3 (0, -3, 0);
		PlayerHolder1.transform.rotation = Quaternion.identity;
		PlayerHolder1.ResetPlayer ();

		PlayerHolder2.transform.localPosition = new Vector3 (0, 3, 0);
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 180);
		PlayerHolder2.ResetPlayer ();

		roundText_HUD.text = "Round: " + OfflineManager.Instance.roundNumber;

		//spawn first glove
		SpawnGlove ();
		glovePicked = false;
	}
    
	//sets the roundWins to 0 for both player
	public void NewMatchStart ()
	{
		PlayerHolder1.roundWins = 0;
		PlayerHolder2.roundWins = 0;
	}

	//plays the sound that is passed in as an argument
	public void PlaySound (AudioSource a)
	{
		if (!Mute)
		{
			a.Play ();
		}
	}
}
