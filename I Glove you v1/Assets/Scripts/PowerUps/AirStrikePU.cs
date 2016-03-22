using UnityEngine;
using System.Collections;

public class AirStrikePU : PowerUp
{
	public int noOfStrikes;
	public int damage;
	public Transform strike1;
	public Transform strike2;
	public Transform strike3;
	public FighterJet fighterJet;
	private bool active;


	private Vector2 EnemyPos;


	public override void Player1Picked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (Strike ());
		}
	}

	public override void Player2Picked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (Strike ());
		}
	}

	IEnumerator Strike ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = false;
		strike1.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3, 3), 0);
		strike2.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3, 3), 0);
		strike3.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3, 3), 0);
		fighterJet.gameObject.SetActive (true);
		fighterJet.AIFollow ();

		yield return new WaitForSeconds (.5f);
		strike1.GetComponent<SpriteRenderer> ().enabled = true;
		strike2.GetComponent<SpriteRenderer> ().enabled = true;
		strike3.GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (1f);
		strike1.GetComponent<CircleCollider2D> ().enabled = true;
		strike2.GetComponent<CircleCollider2D> ().enabled = true;
		strike3.GetComponent<CircleCollider2D> ().enabled = true;

		strike1.GetComponent<Animator> ().Play ("blast_strike");
		strike2.GetComponent<Animator> ().Play ("blast_strike");
		strike3.GetComponent<Animator> ().Play ("blast_strike");


		yield return new WaitForSeconds (.5f);
		DeactivatePU ();
	}

	public override void DeactivatePU ()
	{
		active = false;
		strike1.GetComponent<Animator> ().Play ("blast_idle");
		strike2.GetComponent<Animator> ().Play ("blast_idle");
		strike3.GetComponent<Animator> ().Play ("blast_idle");

		strike1.GetComponent<CircleCollider2D> ().enabled = false;
		strike2.GetComponent<CircleCollider2D> ().enabled = false;
		strike3.GetComponent<CircleCollider2D> ().enabled = false;

		strike1.GetComponent<SpriteRenderer> ().enabled = false;
		strike2.GetComponent<SpriteRenderer> ().enabled = false;
		strike3.GetComponent<SpriteRenderer> ().enabled = false;

		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<CircleCollider2D> ().enabled = true;
		base.DeactivatePU ();

	}

}
