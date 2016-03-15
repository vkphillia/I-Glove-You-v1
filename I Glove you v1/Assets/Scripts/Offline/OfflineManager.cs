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
			if (_Instance == null) {                                                                                   
				_Instance = (OfflineManager)FindObjectOfType (typeof(OfflineManager));
			}
			return _Instance;
		}	
	}

	public bool Mute;
	public AudioSource source_Punch;
	public AudioSource source_RoundStart;
	public AudioSource source_Fight;
	public AudioSource source_Round;
	public AudioSource[] source_RoundNumber;
	
	
	public PlayerHolderController PlayerHolder1;
	public PlayerHolderController PlayerHolder2;
	public Transform Player1;
	public Transform Player2;
	public GameObject glove;
	public bool glovePicked;
	public bool PUPicked;
	public GameObject PU;
	public OfflineRoundController RoundPanel;
	public GameObject Player1HUDPanel;
	public GameObject Player2HUDPanel;
	public int roundNumber;
	public GameState currentState;
	public int MaxHealth;
	public Text roundText_HUD;
	public float MaxRoundTimer;
	public Text timerText_HUD;
	public Transform foreground;

	private float roundTimer;
	private Vector3 P1StartPos;
	private Vector3 P2StartPos;


	void OnEnable ()
	{
		currentState = GameState.RoundStart;
		Player1.GetComponent<SpriteRenderer> ().sprite = Player1.GetComponent<OfflinePlayerController> ().mySprites [0];
		Player2.GetComponent<SpriteRenderer> ().sprite = Player2.GetComponent<OfflinePlayerController> ().mySprites [1];
	}

	void Start ()
	{
		P1StartPos = new Vector3 (0, -3, 0);
		P2StartPos = new Vector3 (0, 3, 0);
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;
		ShowRoundPanel ();
		foreground.transform.localScale = new Vector3 (.8f, 0.8f, 1);
		PUPicked = true;
	}

	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("offline menu");
		}
		if (currentState == GameState.Playing) {
			if (glovePicked) {
				StartCoroutine (SpawnGloveCoroutine ());
			}
			if (PUPicked) {
				StartCoroutine (SpawnPUCoroutine ());
			}
			
			//reduce timer
			roundTimer -= Time.deltaTime;
			timerText_HUD.text = "Time: " + roundTimer.ToString ("N0");
			if (roundTimer <= 0) {
				//Times up and round is over
				if (PlayerHolder1.myHealth > PlayerHolder2.myHealth) {
					PlayerHolder1.roundWins++;
					if (PlayerHolder1.roundWins == 2) {
						currentState = GameState.MatchOver;
					} else {
						currentState = GameState.RoundOver;
					}
				} else if (PlayerHolder2.myHealth > PlayerHolder1.myHealth) {
					PlayerHolder2.roundWins++;
					if (PlayerHolder2.roundWins == 2) {
						currentState = GameState.MatchOver;
					} else {
						currentState = GameState.RoundOver;
					}
				} else {
					currentState = GameState.RoundOver;
				}
				ShowRoundPanel ();
			}
		} else if (currentState == GameState.Fight) {
			ZoomIn ();
		} else if (currentState == GameState.RoundOver || currentState == GameState.MatchOver) {
			ZoomOut ();
			StopCoroutine (SpawnGloveCoroutine ());
			StopCoroutine (SpawnPUCoroutine ());
		}
	}

	public IEnumerator SpawnGloveCoroutine ()
	{
		glovePicked = false;
		yield return new WaitForSeconds (10f);
		SpawnGlove ();
	}

	void SpawnGlove ()
	{
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0);
	}

	public IEnumerator SpawnPUCoroutine ()
	{
		PUPicked = false;
		yield return new WaitForSeconds (5f);
		SpawnPU ();
	}

	void SpawnPU ()
	{
		PU.SetActive (true);
		PU.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0);
	}

	public void StartNewRound ()
	{
		roundTimer = MaxRoundTimer;
		roundNumber++;
		PlayerHolder1.ResetPlayer ();
		PlayerHolder2.ResetPlayer ();
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 180);
		roundText_HUD.text = "Round: " + OfflineManager.Instance.roundNumber;
		//spawn first glove
		SpawnGlove ();
		glovePicked = false;
	}

	public void NewMatchStart ()
	{
		PlayerHolder1.roundWins = 0;
		PlayerHolder2.roundWins = 0;
	}

	public void ShowRoundPanel ()
	{
		RoundPanel.gameObject.SetActive (true);
		if (currentState == GameState.RoundStart) {
			StartCoroutine (RoundPanel.HideRoundStartText ());
		} else if (currentState == GameState.RoundOver) {
			StartCoroutine (RoundPanel.HideRoundOverText ());
		} else if (currentState == GameState.MatchOver) {
			StartCoroutine (RoundPanel.HideMatchOverText ());
		}
	}

	void ZoomIn ()
	{
		Player1HUDPanel.SetActive (true); 
		Player2HUDPanel.SetActive (true); 
		if (foreground.transform.localScale.x < 1) {
			foreground.transform.localScale += new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	public void ZoomOut ()
	{
		Player1HUDPanel.SetActive (false); 
		Player2HUDPanel.SetActive (false); 

		if (foreground.transform.localScale.x > 0.8f) {
			foreground.transform.localScale -= new Vector3 (.2f, 0.2f, 0) * Time.deltaTime;
		}
	}

	public void PlaySound (AudioSource a)
	{
		if (!Mute) {
			a.Play ();
		}
	}
	
}
