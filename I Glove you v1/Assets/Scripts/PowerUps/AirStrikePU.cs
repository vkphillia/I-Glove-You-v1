﻿using UnityEngine;
using System.Collections;

public class AirStrikePU : PowerUp
{
	public int noOfStrikes;
	public int damage;
	public Strike[] AllStrikesArr;
	public Strike strikePrefab;

	public FighterJet fighterJet;
	private bool active;
	private Vector2 EnemyPos;


	void Awake ()
	{
		//Debug.Log ("I am Awake");
		AllStrikesArr = new Strike[noOfStrikes]; 
		for (int i = 0; i < noOfStrikes; i++)
		{
			Strike temp = Instantiate (strikePrefab)as Strike;
			AllStrikesArr [i] = temp;
		}     
	}

	public override void Player1Picked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (StrikeNow ());
		}
	}

	public override void Player2Picked ()
	{
		if (!active)
		{
			active = true;
			StartCoroutine (StrikeNow ());
		}
	}

	IEnumerator StrikeNow ()
	{
		SoundsController.Instance.PlaySoundFX ("GlovePick", 1.0f);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = false;
		for (int i = 0; i < noOfStrikes; i++)
		{
			AllStrikesArr [i].gameObject.SetActive (true); 
		}
		fighterJet.gameObject.SetActive (true);
		fighterJet.AIFollow ();

		yield return new WaitForSeconds (2f);
		DeactivatePU ();
	}

	public override void DeactivatePU ()
	{
		active = false;
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<CircleCollider2D> ().enabled = true;
		base.DeactivatePU ();

	}

	void OnDisable ()
	{
		for (int i = 0; i < noOfStrikes; i++)
		{
			AllStrikesArr [i].myChildBlast.GetComponent<Animator> ().Play ("blast_idle");
			AllStrikesArr [i].myChildBlast.gameObject.SetActive (false);
			AllStrikesArr [i].GetComponent<SpriteRenderer> ().enabled = false;
			AllStrikesArr [i].gameObject.SetActive (false);
		}
	}

}