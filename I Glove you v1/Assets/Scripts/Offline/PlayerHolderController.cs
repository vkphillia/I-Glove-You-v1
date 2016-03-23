using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerHolderController : MonoBehaviour
{
	

	[HideInInspector]	
	public bool hit;
	[HideInInspector]	
	public bool hitter;
	//[HideInInspector]
	public int myHealth;
	[HideInInspector]
	public int roundWins;
	[HideInInspector]
	public bool hasGlove;

	public Sprite[] mySprites;
	public Animator myPunchAnim;

	public SpriteRenderer HitEffectSprite;

	public Text myWinText_HUD;
	public Text myHealthText_HUD;


	public float mySpeed;

	private Vector3 force;
	private bool PUHitter;


	void Start ()
	{
		
		myHealth = OfflineManager.Instance.MaxHealth;
		mySpeed = OfflineManager.Instance.MaxSpeed;
		myHealthText_HUD.text = " Health: " + myHealth;

	}

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -3.7f, 3.7f), 0);

			if (!hit && !hitter && !PUHitter)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
			}
			else if (hit)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + 2);
				StartCoroutine (MakeHitFalse ());

			}
			else if (hitter)
			{
				transform.position += transform.up * Time.deltaTime * (-mySpeed + 1);
				StartCoroutine (MakeHitterFalse ());
			}
			else if (PUHitter)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + .5f);
				StartCoroutine (MakePUHitterFalse ());
			}
		} 
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) // this = player1, other= player2
		{   
			//Debug.Log ("Player 1 gets punched");
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder2.Punch ();

		}
         
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
		{
			//Debug.Log ("Player 2 gets punched");
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder1.Punch ();
		}	
	}


	//this ensures that the player is going in its forward direction after being punched
	IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.5f);
		HitEffectSprite.enabled = false;
		hit = false;
	}

	//this ensures that the player is going int its forward direction after hitting other player
	IEnumerator MakeHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hitter = false;
	}

	IEnumerator MakePUHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		PUHitter = false;
	}

	//Reset on new Round/Match
	public void ResetPlayer ()
	{
		gameObject.SetActive (true);		
		myWinText_HUD.text = "Wins: " + roundWins + "/2";
		hit = false;
		hitter = false;
		myHealth = OfflineManager.Instance.MaxHealth;
		myHealthText_HUD.text = " Health: " + myHealth;
		myPunchAnim.gameObject.SetActive (false);
		HitEffectSprite.enabled = false;
		mySpeed = OfflineManager.Instance.MaxSpeed;
	}

	public void getPunched (Transform t)
	{
		hit = true;
		HitEffectSprite.transform.SetParent (this.transform);
		HitEffectSprite.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		HitEffectSprite.enabled = true;
		transform.rotation = t.rotation; 
		if (myHealth > 0)
		{
			AlterHealth (-1);
		}	
	}


	//punch other objects and player
	public void Punch ()
	{
		hitter = true;
		StartCoroutine (PlayPunchAnim ());
		SoundsController.Instance.PlaySoundFX ("Punch", 1.0f);

	}

	public void PunchPUS ()
	{
		PUHitter = true;
		StartCoroutine (PlayPunchAnim ());
		SoundsController.Instance.PlaySoundFX ("BreakPU", 1.0f);
	}

	IEnumerator PlayPunchAnim ()
	{
		myPunchAnim.Play ("Punch_Hit");
		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}

	//removes glove from player when other player get glove
	public void LoseGlove ()
	{
		hasGlove = false;
		myPunchAnim.gameObject.SetActive (false);
	}

	//adds glove to player when other player loses glove
	public void AddGlove ()
	{
		SoundsController.Instance.PlaySoundFX ("GlovePick", 1.0f);
		hasGlove = true;
		myPunchAnim.gameObject.SetActive (true);
	}

	//increase or decreases the health of the player based on the amount
	public void AlterHealth (int amount)
	{
		if ((myHealth + amount) > OfflineManager.Instance.MaxHealth)
		{
			myHealth = OfflineManager.Instance.MaxHealth;
		}
		else if ((myHealth + amount) <= 0)
		{
			myHealth = 0;
			//CheckForRoundOver();
			//code for checking who wins the round and stops the round
			OfflineManager.Instance.CheckRoundStatus ();
		}
		else
		{
			myHealth += amount;
			//only play sound when adding health
			if (amount > 0)
			{
				SoundsController.Instance.PlaySoundFX ("HealthUp", 1.0f); 
			}
		}
		myHealthText_HUD.text = "Health " + myHealth;
	}

	//Checks for win/loss
	//public void CheckForRoundOver (/*Transform otherPlayer*/)
	//{
	//if (myHealth == 0)
	//{
	//increasing round wins from multiple places not fair
	//round wins increment only from the function checkRoundStatus in offline manager
	//otherPlayer.GetComponentInParent<PlayerHolderController> ().roundWins++;
	//if (otherPlayer.GetComponentInParent<PlayerHolderController> ().roundWins < 2)
	//{
	//OfflineManager.Instance.currentState = GameState.RoundOver;
	//why do they need to call the round panel, its not fair
	//OfflineManager.Instance.ShowRoundPanel ();
	//call this instead
	//	OfflineManager.Instance.CheckRoundStatus ();
	//}
	//else
	//{
	//OfflineManager.Instance.currentState = GameState.MatchOver;
	//why do they need to call the round panel, its not fair
	//OfflineManager.Instance.ShowRoundPanel ();
	//call this instead
	//	OfflineManager.Instance.CheckRoundStatus ();
	//}
	//Player lose animation
	//this.gameObject.SetActive (false);
	//}
	//    OfflineManager.Instance.CheckRoundStatus();
	//}
}