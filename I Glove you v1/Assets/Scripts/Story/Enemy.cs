using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public int health;
	public int maxHealth;
	public float enemySpeed;

	//AI stuff
	//temporary variable, this should be linked to the challenges I guess
	//[HideInInspector]
	public bool AIOn;

	[HideInInspector]
	public bool hasGlove;

	[HideInInspector]
	public bool hit;

	[HideInInspector]
	public bool hitter;

	public Transform myPlayer;
	//temporary variable, get reference from where ever PU is spawning
	public Transform PUOnScreen;

	private Vector3 EnemyPos;
	private Vector3 PlayerPos;
	//temporary variable, get reference from where ever PU is spawning
	private bool PUReady;
	private Vector3 PUPos;
	private Vector3 relativePos;
	private bool destReached;
	private Vector3 randPos;
	private float angle;

	//temp var, jsut checking
	private Challenge myChallenge;

	private bool PUInRange;


	void Awake ()
	{
		myChallenge = GameObject.FindObjectOfType<Challenge> ();
	}

	void Start ()
	{
		//health = 1;//default value, can be changed as required
		destReached = true;

		maxHealth = health;
        
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("enemy has glove:" + hasGlove);

		if (other.gameObject.layer == 8) //layer 8 is player1
		{
			if (hasGlove)
			{
				hitter = true;
				StartCoroutine (MakeHitterFalse ());
			}
			else
			{
				hit = true;
                
				if (health != 1)
				{
					transform.rotation = other.transform.rotation;
					StartCoroutine (MakeHitFalse ());
				}
				if (health > 0)
				{
					AlterHealth (-1);
				}
			}
		}
	}


	//increase or decreases the health of the player based on the amount
	//this is called by glove, power ups
	public void AlterHealth (int amount)
	{
		health += amount;
        
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		else if (health <= 0)
		{
			Challenge.noOfEnemyAlive--;
			gameObject.SetActive (false);
		}
		else
		{
			//only play sound when adding health
			if (amount > 0)
			{
				SoundsController.Instance.PlaySoundFX ("HealthUp", 1.0f);
				StartCoroutine (ChangeColor (Color.green));
			}
			else
			{
				StartCoroutine (ChangeColor (Color.red));
			}
		}
		//myHealthText_HUD.text = myHealth.ToString();
	}

	//this ensures that the player is going in its forward direction after being punched
	IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hit = false;
	}

	//this ensures that the player is going int its forward direction after hitting other player
	IEnumerator MakeHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hitter = false;
	}

	IEnumerator ChangeColor (Color C)
	{
		GetComponent<SpriteRenderer> ().color = C;
		yield return new WaitForSeconds (.5f);
		GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public void Initialize ()
	{
		StartCoroutine (FadeEnemy ());
	}

	IEnumerator FadeEnemy ()
	{
		Color tempColor = GetComponent<SpriteRenderer> ().color;
		tempColor.a = 0;
		GetComponent<SpriteRenderer> ().color = tempColor;
		yield return new WaitForSeconds (1f);
		while (tempColor.a <= 1)
		{
			GetComponent<SpriteRenderer> ().color = tempColor;
			tempColor.a += 0.1f;
			yield return new WaitForSeconds (.1f);
		}
	}



	//AI code starts from here..........................................
	void Update ()
	{
		

		if (AIOn && GameTimer.Instance.timerStarted)
		{
			//this makes it move
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -4.4f, 4.4f), 0);
			transform.position += transform.up * Time.deltaTime * enemySpeed;

			if (hasGlove)
			{
				AIFollowOpponent (); //deactivated for testing, please reactvate later
			}
			else
			{
				AIAvoidOpponent ();
			}
		}
	}

	//Avoids opponent and tries finding power ups
	void AIAvoidOpponent ()
	{
		EnemyPos = Camera.main.WorldToScreenPoint (this.transform.position);

		//find PU
		if (PUReady)
		{
			if (destReached)
			{
				//Find PU and rotate towards that point
				destReached = false;//until it reaches the PU
				PUPos = Camera.main.WorldToScreenPoint (PUOnScreen.transform.position);
				relativePos = PUPos - EnemyPos;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90) * Time.deltaTime * enemySpeed));
			}
			if (EnemyPos == PUPos)
			{
				destReached = true;//make this true if opponent is in range using a bigger trigger
			}

		}
		else
		{
			//Find random point away from player and rotate towards that point
			if (destReached)
			{
				Debug.Log ("find new dest");
				destReached = false;//until it reaches the below new destination
				randPos = Camera.main.WorldToScreenPoint (new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0));
				Debug.Log (randPos);
				relativePos = randPos - EnemyPos;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				this.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90) * Time.deltaTime * enemySpeed));
				Debug.Log (angle);
			}
			if (EnemyPos == randPos)
			{
				destReached = true;//make this true if opponent is in range using a bigger trigger
				Debug.Log ("destReached");
			}
		}


	}

	//follow opponent and destroy all possible PU on the way
	//later we will add code to avoid the bombs and air strikes
	void AIFollowOpponent ()
	{

		if (PUReady && PUInRange) //PUInRange will be determined by a larger Trigger area around the enemy
		{
			PUPos = Camera.main.WorldToScreenPoint (PUOnScreen.transform.position);
			relativePos = PUPos - EnemyPos;
		}
		else
		{
			Debug.Log ("PLAYER pos " + myChallenge.player.transform.position);
			PlayerPos = Camera.main.WorldToScreenPoint (myChallenge.player.transform.position);
		}
		Debug.Log ("Enemy pos " + this.transform.position);
		EnemyPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = PlayerPos - EnemyPos;
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));

	}
}