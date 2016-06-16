using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerHolderController : MonoBehaviour
{
	#region Variables

	[Space]
	public int playerID;
	public int MaxHealth;
	public float MaxSpeed;
	public int myHealth;
	public float mySpeed;
	public int myDamage;
	[Space]
	[Space]
	public Sprite[] mySprites;
	public RuntimeAnimatorController[] animationController;
	public Animator myPunchAnim;
	public Animator myPunchReadyAnim;
	public Animator myWalkAnim;
	public Text myWinText_HUD;
	public Text myHealthText_HUD;

	public Image myFighterImage;

	//for health
	//public Transform myFlyingTextSpawnPoint;
	//new health meter
	public Image myHealthBar;
	public Animator myHealhBarAnim;
	public GameObject myHealthPS;

	//public variables but hidden in inspector below this

	[HideInInspector]	
	public bool isTurning;
	[HideInInspector]	
	public bool hit;
	[HideInInspector]	
	public bool hitter;
	[HideInInspector]	
	public bool lyingDead;
	[HideInInspector]
	public int roundWins;
	[HideInInspector]
	public bool hasGlove;
	[HideInInspector]
	public Color StartingSpriteColor;

    

	//all private varaibles Below this

	private bool justRobbed;
	//private CircleCollider2D myCollider;
	private SpriteRenderer mySprite;
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

	float upTimer;
	float downTimer;
	private int noOfJumps;
	private bool WinStarted;


	#endregion

	void Start ()
	{
		mySprite = GetComponent<SpriteRenderer> ();
		myHealth = MaxHealth;
		mySpeed = MaxSpeed;
		myHealthText_HUD.text = myHealth.ToString ();
		//myCollider = GetComponent<CircleCollider2D> ();
		myWalkAnim = GetComponent<Animator> ();


	}



	void Update ()
	{
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.5f, 2.5f), Mathf.Clamp (transform.position.y, -3.2f, 3.2f), 0);

		//transform.position = new Vector3 (Mathf.Clamp (transform.position.x, OfflineManager.Instance.leftBorder.position.x, OfflineManager.Instance.rightBorder.position.x), Mathf.Clamp (transform.position.y, OfflineManager.Instance.botBorder.position.y, OfflineManager.Instance.topBorder.position.y), 0);


		//old controls
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{

			if (!hit && !hitter && isTurning && !PUHitter && !lyingDead)
			{

				if (OfflineManager.Instance.test_speedChange)
				{
					float tempSpeed = mySpeed - 2;
					transform.position += transform.up * Time.deltaTime * (tempSpeed);
				}
				else
				{
					transform.position += transform.up * Time.deltaTime * (mySpeed);
				}


			}
			else if (!hit && !hitter && !isTurning && !PUHitter && !lyingDead)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
			}
			else if (hit && !lyingDead)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + 1);
				StartCoroutine (MakeHitFalse ());

			}
			else if (hitter && !lyingDead)
			{
				transform.Rotate (0, 0, 1);
				transform.position += transform.up * Time.deltaTime * (-mySpeed);
				StartCoroutine (MakeHitterFalse ());
			}
			else if (PUHitter && !lyingDead)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + .5f);
				StartCoroutine (MakePUHitterFalse ());
			}

		}
		else if (OfflineManager.Instance.currentState == GameState.MatchOver)
		{
			if (!WinStarted)
			{
				WinStarted = true;
				StartCoroutine (WinAnimation ());
			}


		}
		else if (OfflineManager.Instance.currentState == GameState.RoundOver)
		{

			/*//play win animation here
			if (roundWins == 2)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
				transform.Rotate (0, 0, 5);
			}*/

			if (hitter && !lyingDead)
			{
				transform.Rotate (0, 0, 1);
				transform.position += transform.up * Time.deltaTime * (-mySpeed);
				StartCoroutine (MakeHitterFalse ());
			}
			else if (lyingDead)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + 1);

				StartCoroutine (MakeLyingDeadFalse ());
			}
		}


	}



	void OnTriggerEnter2D (Collider2D other)
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			if (this.gameObject.layer == 8 && other.gameObject.layer == 11) // this = player1 without glove, other= player2 with glove
			{
				if (!OfflineManager.Instance.PlayerHolder2.lyingDead)
				{
					getPunched (other.transform);
					OfflineManager.Instance.PlayerHolder2.Punch ();
					AlterHealth (-OfflineManager.Instance.PlayerHolder1.myDamage);
				}

			
			
			}
			else if (this.gameObject.layer == 8 && other.gameObject.layer == 17) // this = player1, other= player2
			{
				if (OfflineManager.Instance.PlayerHolder2.hasGlove && !OfflineManager.Instance.PlayerHolder1.lyingDead)
				{
					//OfflineManager.Instance.PlayerHolder2.justRobbed = true;
					//OfflineManager.Instance.PlayerHolder2.lyingDead = true;

					OfflineManager.Instance.PlayerHolder2.LoseGlove ();
					OfflineManager.Instance.PlayerHolder1.AddGlove ();
					StartCoroutine (OfflineManager.Instance.PlayerHolder2.MakeLyingDeadFalse ());

				}
			}


         
			if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
			{
				if (!OfflineManager.Instance.PlayerHolder1.lyingDead)
				{
					getPunched (other.transform);
					OfflineManager.Instance.PlayerHolder1.Punch ();
					AlterHealth (-OfflineManager.Instance.PlayerHolder2.myDamage);
				}
			}
			else if (this.gameObject.layer == 10 && other.gameObject.layer == 16) // this = player1, other= player2
			{
				if (OfflineManager.Instance.PlayerHolder1.hasGlove && !OfflineManager.Instance.PlayerHolder2.lyingDead)
				{
					OfflineManager.Instance.PlayerHolder1.LoseGlove ();
					OfflineManager.Instance.PlayerHolder2.AddGlove ();
					StartCoroutine (OfflineManager.Instance.PlayerHolder1.MakeLyingDeadFalse ());
				
				}
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

	IEnumerator MakeJustRobbedFalse ()
	{
		yield return new WaitForSeconds (1f);
		lyingDead = false;
		//myCollider.enabled = true;

	}


	//Reset on new Round/Match
	public void ResetPlayer ()
	{
		
		gameObject.SetActive (true);	
		myWalkAnim.Play ("Idle");
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
		myHealthBar.color = Color.green;
		myHealhBarAnim.Play ("HealthBar_Idle");
	}

	public void getPunched (Transform t)
	{
		hit = true;
		transform.rotation = t.rotation; 
	}


	//punch other objects and player
	public void Punch ()
	{
		hitter = true;
		StartCoroutine (PlayPunchAnim ());
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("Punch", 0.15f);

	}

	public void PunchPUS (Transform PU)
	{
		PUHitter = true;
		StartCoroutine (PlayPunchAnim ());
		SpawnPunchPU_FX (PU);
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("BreakPU", 0.15f);

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
		GloveDisappear ();
	}

	void GloveDisappear ()
	{
		myPunchAnim.gameObject.SetActive (false);
		myPunchReadyAnim.Play ("Punch_Disappear");
		myWalkAnim.Play ("WalkNoGlove");
	}



	//adds glove to player when other player loses glove
	public void AddGlove ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("GlovePick", 0.15f);
		hasGlove = true;
		StartCoroutine (GloveAppear ());
	}


	IEnumerator GloveAppear ()
	{
		Debug.Log ("playing GloveAppear anim");
		myPunchReadyAnim.Play ("Punch_Appear");
		yield return new WaitForSeconds (0.5f);
		myPunchAnim.gameObject.SetActive (true);
		myPunchAnim.Play ("Punch_Idle");
		myWalkAnim.Play ("WalkGlove");
	}

	//increase or decreases the health of the player based on the amount
	public void AlterHealth (int amount)
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			myPooledFT = GameObjectPool.GetPool ("FlyingTextPool").GetInstance ();
			FT_Obj = myPooledFT.GetComponent<FlyingText> ();
			//FT_Obj.transform.SetParent (this.transform);
			if (playerID == 1)
			{
				FT_Obj.transform.position = new Vector3 (-0.94f, -4.19f, 0);
				FT_Obj.transform.rotation = Quaternion.identity;
			}
			else
			{
				FT_Obj.transform.position = new Vector3 (1.27f, 4.31f, 0);
				FT_Obj.transform.rotation = Quaternion.Euler (0, 0, 180);
			}
			
			//new health bar

			if (amount > 0)
			{
				FT_Obj.myGreenText.color = Color.green;
				FT_Obj.myBlackText.text = "+" + amount.ToString ();
				FT_Obj.myGreenText.text = "+" + amount.ToString ();
				StartCoroutine (SpawnHealthPS ());
			}
			else
			{
				FT_Obj.myGreenText.color = Color.red;
				FT_Obj.myBlackText.text = amount.ToString ();
				FT_Obj.myGreenText.text = amount.ToString ();
				SpawnHit_FX ();
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
				myHealthBar.color = Color.green;
				myHealhBarAnim.Play ("HealthBar_Idle");
			}
			else
			{
				myHealth += amount;
				myHealthBar.fillAmount = (float)(myHealth) / MaxHealth; 
				//SpawnHit_FX ();
				//only play sound when adding health
				if (amount > 0)
				{
					if (SoundsController.Instance != null)
						SoundsController.Instance.PlaySoundFX ("HealthUp", 0.15f); 
					StartCoroutine (ChangeColor (Color.green));
				}
				else
				{
					StartCoroutine (ChangeColor (Color.red));
				}
			}

		}
		myHealthText_HUD.text = myHealth.ToString ();
	}

	IEnumerator SpawnHealthPS ()
	{
		myHealthPS.SetActive (true);
		yield return new WaitForSeconds (2f);
		myHealthPS.SetActive (false);

	}

	IEnumerator ChangeColor (Color C)
	{
		if (hit)
		{
			myWalkAnim.Play ("Hit");
	
		}
		else
		{
			mySprite.color = C;
		}
		myHealthBar.color = C;
		myHealhBarAnim.Play ("HealthBar_Shake");
		yield return new WaitForSeconds (.3f);
		//myHealhBarAnim.Play ("HealthBar_Idle");
		//low health
		if (myHealth < 4 && myHealth > 0)
		{
			myHealthBar.color = Color.red;
			myHealhBarAnim.Play ("HealthBar_Low");
		}
		else
		{
			myHealthBar.color = Color.green;
			myHealhBarAnim.Play ("HealthBar_Idle");
		}
            

		yield return new WaitForSeconds (.3f);
		mySprite.color = StartingSpriteColor;
		if (hasGlove)
		{
			myWalkAnim.Play ("WalkGlove");


		}
		if (!hasGlove)
		{
			myWalkAnim.Play ("WalkNoGlove");
		}

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

	public IEnumerator MakeLyingDeadFalse ()
	{
		lyingDead = true;
		myWalkAnim.Play ("Dead");
		yield return new WaitForSeconds (1.5f);


		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			if (hasGlove)
			{
				myWalkAnim.Play ("WalkGlove");


			}
			if (!hasGlove)
			{
				myWalkAnim.Play ("WalkNoGlove");
			}


		}
		lyingDead = false; //start moving forward

	}

	IEnumerator WinAnimation ()
	{
		yield return new WaitForSeconds (0.5f);
		OfflineManager.Instance.PlayerHolder1.transform.position = new Vector3 (0, -1.5f, 0);
		OfflineManager.Instance.PlayerHolder2.transform.position = new Vector3 (0, 1.5f, 0);
		OfflineManager.Instance.PlayerHolder1.transform.rotation = Quaternion.Euler (0, 0, 0);
		OfflineManager.Instance.PlayerHolder2.transform.rotation = Quaternion.Euler (0, 0, -180);
		LoseGlove ();

		myWalkAnim.Play ("Idle");
		//play win animation here

		if (roundWins == 2)
		{
			/*while (noOfJumps < 5)
			{
				if (upTimer >= 0.5f)
				{
					//go down
					if (downTimer < 0.3f)
					{
						downTimer -= Time.deltaTime;
						transform.position = transform.up * Time.deltaTime * -mySpeed;//go down
					}
					else
					{
						upTimer = 0;
						downTimer = 0;
						noOfJumps++;
					}

				}
				else
				{
					upTimer += Time.deltaTime;
					transform.position = transform.up * Time.deltaTime * mySpeed;//go up
				}







				//transform.position = transform.up * Time.deltaTime * mySpeed;
				//transform.Rotate (0, 0, 5);
			}*/
			
		}
	}





}