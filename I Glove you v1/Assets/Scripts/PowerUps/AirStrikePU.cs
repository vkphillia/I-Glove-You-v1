using UnityEngine;
using System.Collections;

public class AirStrikePU : PowerUp
{
	public int noOfStrikes;
	public int damage;
	public Strike[] AllStrikesArr;
	public Strike strikePrefab;

	public FighterJet fighterJet;

	private Vector2 EnemyPos;

	private SpriteRenderer mySpriteRenderer;
	private Collider2D myCol;




	void Awake ()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myCol = GetComponent<Collider2D> ();

		AllStrikesArr = new Strike[noOfStrikes]; 
		for (int i = 0; i < noOfStrikes; i++)
		{
			Strike temp = Instantiate (strikePrefab)as Strike;
			AllStrikesArr [i] = temp;
			//AllStrikesArr [i].transform.SetParent (this.transform);
		}     
	}

	public override void Player1Picked ()
	{
		if (!active)
		{
			myAnim.Play ("PU_Idle");
		
			myPS.gameObject.SetActive (false);

			active = true;
			StartCoroutine (StrikeNow ());
		}
	}

	public override void Player2Picked ()
	{
		if (!active)
		{
			myAnim.Play ("PU_Idle");
		
			myPS.gameObject.SetActive (false);

			active = true;
			StartCoroutine (StrikeNow ());
		}
	}



	IEnumerator StrikeNow ()
	{
		myCol.enabled = false;
		mySpriteRenderer.enabled = false;

		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("AirStrike", 0.3f);
		for (int i = 0; i < noOfStrikes; i++)
		{
			AllStrikesArr [i].gameObject.SetActive (true); 
			Debug.Log ("SpawnStrike " + i);
			AllStrikesArr [i].gameObject.layer = 15;
		}
		fighterJet.gameObject.SetActive (true);
		fighterJet.AIFollow ();

		yield return new WaitForSeconds (3f);
		DeactivatePU ();
	}

	public override void DeactivatePU ()
	{
		if (active)
		{
			active = false;
			mySpriteRenderer.enabled = true;
			myCol.enabled = true;
		}
		base.DeactivatePU ();
	}

	void OnDisable ()
	{
		for (int i = 0; i < noOfStrikes; i++)
		{
			if (AllStrikesArr [i].BlastActive)
			{
				AllStrikesArr [i].myChildBlast.myBlastAnim.Play ("blast_idle");
				AllStrikesArr [i].myChildBlast.gameObject.SetActive (false);
			}
			AllStrikesArr [i].gameObject.SetActive (false);
		}
	}
}
