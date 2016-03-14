using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
	RoundStart,
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

	
	public PlayerHolderController PlayerHolder1;
	public PlayerHolderController PlayerHolder2;
	public Transform Player1;
	public Transform Player2;
	public GameObject glove;
	public bool glovePicked;
	public OfflineRoundController RoundPanel;
	public int roundNumber;
	public GameState currentState;
	public int MaxHealth;
	public Text roundHUDText;
	

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
		P1StartPos = new Vector3 (0, -4, 0);
		P2StartPos = new Vector3 (0, 4, 0);
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;
		ShowRoundPanel ();
	}

	void Update ()
	{
		if (glovePicked && currentState == GameState.Playing) {
			StartCoroutine (SpawnGloveCoroutine ());
		} else if (currentState != GameState.Playing) {
			StopCoroutine (SpawnGloveCoroutine ());
		}
	}

	public IEnumerator SpawnGloveCoroutine ()
	{
		glovePicked = false;
		Debug.Log ("Spawned");
		yield return new WaitForSeconds (10f);
		SpawnGlove ();
	}

	void SpawnGlove ()
	{
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4f, 4f), 0);
	}

	public void StartNewRound ()
	{
		roundNumber++;
		PlayerHolder1.ResetPlayer ();
		PlayerHolder2.ResetPlayer ();
		PlayerHolder1.transform.position = P1StartPos;
		PlayerHolder2.transform.position = P2StartPos;
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 180);
		roundHUDText.text = "Round: " + OfflineManager.Instance.roundNumber;
		//spawn first glove
		SpawnGlove ();
		glovePicked = false;
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

	
}
