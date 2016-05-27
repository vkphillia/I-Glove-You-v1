using UnityEngine;
using System.Collections;

public class GloveController : PowerUp
{
	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
	}

	public override void Player1Picked ()
	{
		OfflineManager.Instance.PlayerHolder1.AddGlove ();
		OfflineManager.Instance.PlayerHolder2.LoseGlove ();
		OfflineManager.Instance.glovePicked = true;
		
		base.DeactivatePU ();
	}

	public override void Player2Picked ()
	{
		
		OfflineManager.Instance.PlayerHolder1.LoseGlove ();
		OfflineManager.Instance.PlayerHolder2.AddGlove ();
		OfflineManager.Instance.glovePicked = true;
		
		base.DeactivatePU ();
	}


	public override void Player1WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
		OfflineManager.Instance.glovePicked = true;
		base.DeactivatePU ();
	}

	public override void Player2WithGlovePicked ()
	{
		OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
		OfflineManager.Instance.glovePicked = true;
		base.DeactivatePU ();
	}



	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
