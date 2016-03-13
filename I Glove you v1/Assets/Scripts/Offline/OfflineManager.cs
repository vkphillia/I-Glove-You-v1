using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
	GetReady,
	Playing,
	Paused,
	RoundOver}
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
	public GameObject MenuPanel;
	public int roundNumber;
	public GameState currentState;
	public int MaxHealth;
	public Text roundText;

	void OnEnable ()
	{
		currentState = GameState.GetReady;
		Player1.GetComponent<SpriteRenderer> ().sprite = Player1.GetComponent<OfflinePlayerController> ().mySprites [0];
		Player2.GetComponent<SpriteRenderer> ().sprite = Player2.GetComponent<OfflinePlayerController> ().mySprites [1];
		MenuPanel.SetActive (true);

	}

	void Awake ()
	{
		PlayerHolderController.OnRoundOver += ResetRound;
	}

	void Start ()
	{
		//spawn first glove
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4f, 4f), 0);
		roundText.text = "Round: " + roundNumber;
	}

	void Update ()
	{
		if (glovePicked && currentState == GameState.Playing) {
			StartCoroutine (SpawnGlove ());
		}
	}

	public IEnumerator SpawnGlove ()
	{
		glovePicked = false;
		Debug.Log ("Spawned");
		yield return new WaitForSeconds (10f);
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4f, 4f), 0);
	}

	public IEnumerator StartNewRound ()
	{
		yield return new WaitForSeconds (5f);
		currentState = GameState.GetReady;
		MenuPanel.SetActive (true);
		PlayerHolder1.ResetPlayer ();
		PlayerHolder2.ResetPlayer ();
		PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, 180);
		
	}

	void ResetRound ()
	{
		currentState = GameState.RoundOver;
		roundNumber++;
		StartCoroutine (StartNewRound ());
	}

	void OnDestroy ()
	{
		PlayerHolderController.OnRoundOver -= ResetRound;
	}
	
}
