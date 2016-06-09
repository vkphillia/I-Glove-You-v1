using UnityEngine;
using System.Collections;

public class WalkingBombPU : PowerUp
{
	public int damageByBlast;
	public float myTime;
	public WalkingBombBlastCol myBlastCol;
	public float mySpeed;

	private Vector3 EnemyPos;
	private Vector3 myPos;
	private Vector3 relativePos;
	private float angle;
	private bool blasted;
	private SpriteRenderer mySpriteRenderer;
	private Collider2D myCol;

	public void Awake ()
	{
		myCol = GetComponent<Collider2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public override void OnEnable ()
	{
		mySpriteRenderer.enabled = true;
		myCol.enabled = true;
		myBlastCol.gameObject.SetActive (false);
		//myBlastCol.mySpriteRenderer.enabled = false;
		//myBlastCol.myCol.enabled = false;
		base.OnEnable ();

	}

	void Update ()
	{
		//find other player and go towards it
		if (active && !blasted)
		{
			if (OfflineManager.Instance.currentState == GameState.Playing)
			{

				AIFollow ();
				transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.75f, 2.75f), Mathf.Clamp (transform.position.y, -3.7f, 3.7f), 0);
				transform.position += transform.up * Time.deltaTime * mySpeed;

			}
		}

	}

	public override void Player1Picked ()
	{
		if (!active)
		{
			myPS.gameObject.SetActive (false);
			active = true;
			StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder1));
		}
	}

	public override void Player2Picked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (ActivateBomb (OfflineManager.Instance.PlayerHolder2));
		}
	}


	public override void Player1WithGlovePicked ()
	{
		if (!active)
		{
			OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public override void Player2WithGlovePicked ()
	{
		if (!active)
		{
			OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
			DeactivatePU ();
		}
	}

	public IEnumerator ActivateBomb (PlayerHolderController p)
	{
		myBlastCol.gameObject.SetActive (true);
		myBlastCol.myAnim.Play ("WalkingBomb_Idle");
		SoundsController.Instance.walkingBomb.Play (); 

		mySpriteRenderer.enabled = false;
		myCol.enabled = false;
		myPS.gameObject.SetActive (false);
		//myBlastCol.mySpriteRenderer.enabled = true;
		//myBlastCol.myCol.enabled = true;
		yield return new WaitForSeconds (myTime);
		//blast
		StartCoroutine (BlastNow (p));
	}

	//explosion stuff goes here
	public IEnumerator BlastNow (PlayerHolderController p)
	{
		blasted = true;
		SoundsController.Instance.walkingBomb.Pause (); 
		SoundsController.Instance.PlaySoundFX ("Blast", 1.0f);
		myBlastCol.myAnim.Play ("WalkingBomb_Blast");
		//myBlastCol.mySpriteRenderer.enabled = false;
		yield return new WaitForSeconds (.5f);
		myBlastCol.myAnim.Play ("WalkingBomb_Idle");
		active = false;
		blasted = false;
		DeactivatePU ();

	}

	public override void DeactivatePU ()
	{

		SoundsController.Instance.walkingBomb.Pause (); 
		transform.rotation = Quaternion.Euler (0, 0, 0);
		active = false;
		//disable sprite and blast collider before disabling gameobject
		//myBlastCol.mySpriteRenderer.enabled = false;
		//myBlastCol.myCol.enabled = false;
		myBlastCol.myAnim.Play ("WalkingBomb_Idle");
		myBlastCol.gameObject.SetActive (false);

		//activate its collider and sprite renderer
		myCol.enabled = true;
		mySpriteRenderer.enabled = true;
		base.DeactivatePU ();
	}

	void AIFollow ()
	{
		
		//Find playr with GLove and follow
		if (OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder1.transform.position);
		}
		else if (OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			EnemyPos = Camera.main.WorldToScreenPoint (OfflineManager.Instance.PlayerHolder2.transform.position);
		}
	
		myPos = Camera.main.WorldToScreenPoint (this.transform.position);
		relativePos = EnemyPos - myPos;

		//rotate towards player with glove
		angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (angle - 90)));
	}
}
