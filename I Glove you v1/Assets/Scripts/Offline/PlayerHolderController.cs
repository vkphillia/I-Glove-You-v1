using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public delegate void PUReadyEvent ();
public class PlayerHolderController : MonoBehaviour
{
	public static event PUReadyEvent OnPUReady;

	[HideInInspector]	
	public bool isTurning;
	[HideInInspector]	
	public bool hit;

	[HideInInspector]	
	public bool hitter;



	[HideInInspector]
	public int roundWins;

	[HideInInspector]
	public bool hasGlove;

	public Sprite[] mySprites;
	public Animator myPunchAnim;

	private SpriteRenderer mySprite;

	public Text myWinText_HUD;
	public Text myHealthText_HUD;



	public int MaxHealth;
	public float MaxSpeed;

	public int myHealth;
	public float mySpeed;
	public int myDamage;

	private Vector3 force;
	private bool PUHitter;

	//for puff effect when punch hits PU
	private Transform myPooledPunchPU_FX;
	private PunchPU_FX PunchPU_Obj;

	//for hit effect when punch hits opponent
	private Transform myPooledHit_FX;
	private GetHit_FX Hit_Obj;

	//for flying text
	private Transform myPooledFT;
	private FlyingText FT_Obj;

	//for health
	public Transform myFlyingTextSpawnPoint;
	//new health meter
	public Image myHealthBar;

	//for speed meter
	public Image mySpeedBar;


	//new punch anim
	public Sprite myPunchSprite;
	private Sprite myOriginalSprite;

	public float myTurningSpeed;





	void Start ()
	{
		mySprite = GetComponent<SpriteRenderer> ();
		myOriginalSprite = mySprite.sprite;
		myHealth = MaxHealth;
		mySpeed = MaxSpeed;
		myHealthText_HUD.text = myHealth.ToString ();


	}

	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.7f, 2.7f), Mathf.Clamp (transform.position.y, -3.8f, 3.7f), 0);
	
		//old controls
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{

			if (!hit && !hitter && isTurning && !PUHitter)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed - 2);
				Debug.Log ("Turning");
			}
			else if (!hit && !hitter && !isTurning && !PUHitter)
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
		if (OfflineManager.Instance.currentState == GameState.MatchOver)
		{
			if (roundWins == 2)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
				transform.Rotate (0, 0, 5);
			}
			else
			{
				gameObject.SetActive (false);
			}
		}
	}



	void OnTriggerEnter2D (Collider2D other)
	{


		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) // this = player1 without glove, other= player2 with glove
		{
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder2.Punch ();
			AlterHealth (-OfflineManager.Instance.PlayerHolder1.myDamage);
		}
		else if (this.gameObject.layer == 8 && other.gameObject.layer == 17) // this = player1, other= player2
		{
			if (OfflineManager.Instance.PlayerHolder2.hasGlove)
			{
				//steal glove
				OfflineManager.Instance.PlayerHolder2.getPunched (other.transform);
				OfflineManager.Instance.PlayerHolder1.AddGlove ();
				OfflineManager.Instance.PlayerHolder2.LoseGlove ();
			}
		}


         
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
		{
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder1.Punch ();
			AlterHealth (-OfflineManager.Instance.PlayerHolder2.myDamage);
		}
		else if (this.gameObject.layer == 10 && other.gameObject.layer == 16) // this = player1, other= player2
		{
			if (OfflineManager.Instance.PlayerHolder1.hasGlove)
			{
				//steal glove
				OfflineManager.Instance.PlayerHolder1.getPunched (other.transform);
				OfflineManager.Instance.PlayerHolder2.AddGlove ();
				OfflineManager.Instance.PlayerHolder1.LoseGlove ();
			}
		}



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

	IEnumerator MakePUHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		PUHitter = false;
	}

	//Reset on new Round/Match
	public void ResetPlayer ()
	{
		
		gameObject.SetActive (true);	
		myWinText_HUD.text = roundWins.ToString ();
		hit = false;
		hitter = false;
		myHealth = MaxHealth;
		mySpeed = MaxSpeed;
		myHealthText_HUD.text = myHealth.ToString ();
		myPunchAnim.gameObject.SetActive (false);
		//HitEffectSprite.enabled = false;
		hasGlove = false; 
		myHealthBar.fillAmount = 1;
		mySpeedBar.fillAmount = 1;
			


	}

	public void getPunched (Transform t)
	{
		hit = true;
		SpawnHit_FX ();
		transform.rotation = t.rotation; 
	}


	//punch other objects and player
	public void Punch ()
	{
		hitter = true;
		StartCoroutine (PlayPunchAnim ());
		SoundsController.Instance.PlaySoundFX ("Punch", 1.0f);

	}

	public void PunchPUS (Transform PU)
	{
		PUHitter = true;
		StartCoroutine (PlayPunchAnim ());
		SpawnPunchPU_FX (PU);
		SoundsController.Instance.PlaySoundFX ("BreakPU", 1.0f);

	}

	IEnumerator PlayPunchAnim ()
	{
		int randPunch = Random.Range (0, 2);
		if (randPunch == 0)
		{
			myPunchAnim.Play ("Punch_Hit");
		
		}
		else
		{
			myPunchAnim.Play ("Punch_Hit2");
		}
		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}

	//removes glove from player when other player get glove
	public void LoseGlove ()
	{
		mySpeed = MaxSpeed;
		hasGlove = false;
		mySprite.sprite = myOriginalSprite;
		myPunchAnim.gameObject.SetActive (false);
	}

	//adds glove to player when other player loses glove
	public void AddGlove ()
	{
		SoundsController.Instance.PlaySoundFX ("GlovePick", 1.0f);
		mySpeed = 3;
		myTurningSpeed = mySpeed - 2;
		hasGlove = true;
		mySprite.sprite = myPunchSprite; 
		myPunchAnim.gameObject.SetActive (true);
		myPunchAnim.Play ("Punch_Idle");
	}

	//increase or decreases the health of the player based on the amount
	public void AlterHealth (int amount)
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			myPooledFT = GameObjectPool.GetPool ("FlyingTextPool").GetInstance ();
			FT_Obj = myPooledFT.GetComponent<FlyingText> ();
			//FT_Obj.transform.SetParent (this.transform);
			FT_Obj.transform.position = myFlyingTextSpawnPoint.position;
			FT_Obj.transform.rotation = myFlyingTextSpawnPoint.rotation;
			//new health bar

			if (amount > 0)
			{
				FT_Obj.myGreenText.color = Color.green;
				FT_Obj.myBlackText.text = "+" + amount.ToString ();
				FT_Obj.myGreenText.text = "+" + amount.ToString ();
			}
			else
			{
				FT_Obj.myGreenText.color = Color.red;
				FT_Obj.myBlackText.text = amount.ToString ();
				FT_Obj.myGreenText.text = amount.ToString ();
			}

			if ((myHealth + amount) > MaxHealth)
			{
				myHealth = MaxHealth;
				myHealthBar.fillAmount = 1f; 

			}
			else if ((myHealth + amount) <= 0)
			{
				myHealth = 0;
				myHealthBar.fillAmount = 0f; 

				//code for checking who wins the round and stops the round
				OfflineManager.Instance.CheckRoundStatus ();
			}
			else
			{
				myHealth += amount;
				myHealthBar.fillAmount = (float)(myHealth) / MaxHealth; 

				//only play sound when adding health
				if (amount > 0)
				{
					SoundsController.Instance.PlaySoundFX ("HealthUp", 1.0f); 
					StartCoroutine (ChangeColor (Color.green));
				}
				else
				{
					StartCoroutine (ChangeColor (Color.red));
					StartCoroutine (ChangeHealthBarColor ());
				}
			}

		}



		myHealthText_HUD.text = myHealth.ToString ();


	}

	public void UpdatePP ()
	{
		/*if (myPowerPoints < 5)
		{
			myPowerPoints++;
			myPowerText_HUD.text = myPowerPoints.ToString ();
		}
		if (myPowerPoints == 5)
		{
			OfflineManager.Instance.myStrike.gameObject.SetActive (true);
			//fire PU
			if (OnPUReady != null)
			{
				OnPUReady ();
			}
			myPowerPoints = 0;
		}*/
		if (mySpeed + 2 < MaxSpeed)
		{
			mySpeed += 2;
		}
		else
		{
			mySpeed = MaxSpeed;
		}

	}

	IEnumerator ChangeHealthBarColor ()
	{
		myHealthBar.color = Color.red;
		yield return new WaitForSeconds (.3f);
		myHealthBar.color = Color.green;
	}


	IEnumerator ChangeColor (Color C)
	{
		mySprite.color = C;
		yield return new WaitForSeconds (.5f);
		mySprite.color = Color.white;
	}

	//Spawining from game object pools
	public void SpawnPunchPU_FX (Transform PU)
	{
		myPooledPunchPU_FX = GameObjectPool.GetPool ("PunchPUPool").GetInstance ();
		PunchPU_Obj = myPooledPunchPU_FX.GetComponent<PunchPU_FX> ();
		PunchPU_Obj.transform.position = PU.position;
		PunchPU_Obj.transform.rotation = Quaternion.identity;
	}

	public void SpawnHit_FX ()
	{
		myPooledHit_FX = GameObjectPool.GetPool ("GetHitPool").GetInstance ();
		Hit_Obj = myPooledHit_FX.GetComponent<GetHit_FX> ();
		Hit_Obj.transform.position = this.transform.position;
		Hit_Obj.transform.rotation = Quaternion.identity;
	}



}