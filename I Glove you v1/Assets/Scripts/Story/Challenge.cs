using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public delegate void StoryGloveEvent ();
public class Challenge : MonoBehaviour
{
	public static event StoryGloveEvent SpwanFirstGlove;

	public static int noOfEnemyAlive;
	//temporary, can be removed later

	//challenge stuff
	[HideInInspector]
	public Button myButton;
	public int myLevelNum;
	public string myLevelDesciption;
	public int NoOfEnemies;

	//enemy stuff
	public Sprite myEnemySprite;
	public float myEnemyMaxSpeed;
	public float myEnemyMaxHealth;
	private int myEnemySpeed;
	private int myEnemyHealth;

	//player stuff
	public float myPlayerMaxSpeed;
	public float myPlayerMaxHealth;
	private int myPlayerSpeed;
	private int myPlayerHealth;

	//round stuff
	public int NoOfRounds;
	[HideInInspector]
	public int roundNumber;
	public float MaxRoundTimer;
	[HideInInspector]
	public float roundTimer;
	//public Text roundText_HUD;
	//public Text timerText_HUD;
	[HideInInspector]
	public bool glovePicked;

	public PlayerControlsUniversal player;
	public EnemyHolder enemyHolder;

	public virtual void Awake ()
	{
		myButton = GetComponent<Button> ();
		roundTimer = MaxRoundTimer;

	}



	public virtual void StartNewRound ()
	{
		roundTimer = MaxRoundTimer;
		roundNumber++;


	}

	public virtual void CheckForObjectiveComplete ()
	{
		//Debug.Log(Check);
	}


	public virtual void CheckRoundStatus ()
	{
		
		if (roundNumber == NoOfRounds)
		{
			StoryManager.Instance.currentState = StoryGameState.MatchOver;
		}
		else
		{
			StoryManager.Instance.currentState = StoryGameState.RoundOver;
		}
		
		StoryManager.Instance.RoundPanel.gameObject.SetActive (true);
	}


	//public virtual void SpawnEnemy ()
	//{
	//	if (NoOfEnemies > 0)
	//	{
	//		StoryManager.Instance.myEnemy.Initialize ();
	//	}

	//}

	//public virtual void SpawnPlayer ()
	//{
	//	StoryManager.Instance.myPlayer.Initialize ();
	//}

	public virtual void Initialize ()
	{
		//add code in individual challenges
	}


}
