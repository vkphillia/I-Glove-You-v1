﻿using UnityEngine;
using System.Collections;

public class GloveController : PowerUp
{





	new void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
	}

	public override void Player1Picked ()
	{

		OfflineManager.Instance.PlayerHolder1.AddGlove ();

		if (OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder2.LoseGlove ();
		}
		
		base.DeactivatePU ();
	}

	public override void Player2Picked ()
	{
	
		if (OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder1.LoseGlove ();
		}
		OfflineManager.Instance.PlayerHolder2.AddGlove ();
		
		base.DeactivatePU ();
	}


	public override void Player1WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
		base.DeactivatePU ();
	}

	public override void Player2WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
		base.DeactivatePU ();
	}

	new void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
