using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public delegate void TriggerEvent ();

public class PlayerHolderController : MonoBehaviour
{
	
	public static event TriggerEvent OnTrigger;

	[HideInInspector]	
	public bool hit;
	[HideInInspector]	
	public bool hitter;
	[HideInInspector]
	public int myHealth;
	[HideInInspector]
	public int roundWins;

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
		PlayerHolderController.OnTrigger += Punch;
	}

	void Start ()
	{
		myHealth = OfflineManager.Instance.MaxHealth;
		mySpeed = 5f;
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

			}
			else if (hitter)
			{
				transform.position += transform.up * Time.deltaTime * (-mySpeed + 1);
			}
		} 
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) // this = player1, other= player2
		{   
			Debug.Log ("Player 1 gets punched");
			StartCoroutine (HitEffect (other.GetComponentInParent<Rigidbody2D> ()));
		}
         
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
		{
			Debug.Log ("Player 2 gets punched");
			StartCoroutine (HitEffect (other.GetComponentInParent<Rigidbody2D> ()));
		}	
	}

	IEnumerator HitEffect (Rigidbody2D r)
	{
		if (OnTrigger != null)
		{
			OnTrigger ();
		}

		r.GetComponent<PlayerHolderController> ().Punch ();
		yield return new WaitForSeconds (0f);
		r.GetComponent<PlayerHolderController> ().hitter = true;
		hit = true;

		StartCoroutine (MakeHitFalse (r));

		HitEffectSprite.transform.SetParent (this.transform);
		HitEffectSprite.gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, -5);
		HitEffectSprite.enabled = true;
		transform.rotation = r.transform.rotation; 

		if (myHealth > 0)
		{
			myHealth--;
			myHealthText_HUD.text = " Health: " + myHealth;
			if (myHealth == 0)
			{
				r.GetComponent<PlayerHolderController> ().roundWins++;
				this.gameObject.SetActive (false);
				if (r.GetComponent<PlayerHolderController> ().roundWins < 2)
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

	public IEnumerator MakeHitFalse (Rigidbody2D r)
	{
		yield return new WaitForSeconds (.3f);
		HitEffectSprite.enabled = false;
		hit = false;
		r.GetComponent<PlayerHolderController> ().hitter = false;
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
	}


	IEnumerator PlayPunchAnim ()
	{
		myPunchAnim.Play ("Punch_Hit");
		
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Punch);
        soundController.PlaySoundFX("Punch");
        

		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}

	public void Punch ()
	{
		StartCoroutine (PlayPunchAnim ());
	}

	void OnDestroy ()
	{
		PlayerHolderController.OnTrigger -= Punch;
	}


}