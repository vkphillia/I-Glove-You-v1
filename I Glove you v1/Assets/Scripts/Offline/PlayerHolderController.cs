using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerHolderController : MonoBehaviour
{
	

	[HideInInspector]	
	public bool hit;
	[HideInInspector]	
	public bool hitter;
	[HideInInspector]
	public int myHealth;
	[HideInInspector]
	public int roundWins;
	[HideInInspector]
	public bool hasGlove;

	public SoundsController soundController;

	public Sprite[] mySprites;
	public Animator myPunchAnim;

	public SpriteRenderer HitEffectSprite;

	public Text myWinText_HUD;
	public Text myHealthText_HUD;


	public float mySpeed;

	private Vector3 force;

	void Awake ()
	{
	}

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

			if (!hit && !hitter)
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

	public void getPunched (Transform t)
	{
		hit = true;
		HitEffectSprite.transform.SetParent (this.transform);
		HitEffectSprite.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		HitEffectSprite.enabled = true;
		transform.rotation = t.rotation; 
		if (myHealth > 0)
		{
			myHealth--;
			myHealthText_HUD.text = " Health: " + myHealth;
			if (myHealth == 0)
			{
				t.GetComponentInParent<PlayerHolderController> ().roundWins++;
				this.gameObject.SetActive (false);
				if (t.GetComponentInParent<PlayerHolderController> ().roundWins < 2)
				{
					OfflineManager.Instance.currentState = GameState.RoundOver;
					OfflineManager.Instance.ShowRoundPanel ();
				}
				else
				{
					OfflineManager.Instance.currentState = GameState.MatchOver;
					OfflineManager.Instance.ShowRoundPanel ();
				}
			}
		}	

	}



	IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.5f);
		HitEffectSprite.enabled = false;
		hit = false;
	}

	IEnumerator MakeHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hitter = false;
	}


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


	IEnumerator PlayPunchAnim ()
	{
		myPunchAnim.Play ("Punch_Hit");
		soundController.PlaySoundFX ("Punch");
		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}

	public void Punch ()
	{
		hitter = true;
		StartCoroutine (PlayPunchAnim ());
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
		hasGlove = true;
		myPunchAnim.gameObject.SetActive (true);
	}

}