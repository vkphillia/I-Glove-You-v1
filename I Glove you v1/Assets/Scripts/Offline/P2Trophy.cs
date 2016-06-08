﻿using UnityEngine;
using System.Collections;

public class P2Trophy : P2HUD
{
	private Animator myTrophyAnim;

	void Awake ()
	{
		myTrophyAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		transform.position = new Vector3 (0, 1.6f, -1);
		StartCoroutine (MakeItFly ());
	}

	IEnumerator MakeItFly ()
	{
		yield return new WaitForSeconds (1f);
		myTrophyAnim.Play ("Trophy_Show");
		SoundsController.Instance.PlaySoundFX ("GlovePick", 1f);
		yield return new WaitForSeconds (1f);
        StartCoroutine(SmoothPositionMovement.Instance.MoveGameObject(this.gameObject, new Vector3(0f, 4.3f, -1), 1f));
        //iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3 (0f, 4.3f, -1), "time", 1f, "easetype", "linear", "onComplete", "DestoryGO"));
        yield return new WaitForSeconds(1f);
        DestoryGO();

    }

	void DestoryGO ()
	{
		//OfflineManager.Instance.PlayerHolder2.myWinText_HUD.text = OfflineManager.Instance.PlayerHolder2.roundWins.ToString ();
		if (OfflineManager.Instance.PlayerHolder2.roundWins == 1)
		{
			trophies [0].SetActive (true);
		}
		else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
		{
			trophies [0].SetActive (true);
			trophies [1].SetActive (true);
		}
		SoundsController.Instance.PlaySoundFX ("CollectPoint", 1f);
		myTrophyAnim.Play ("Trophy_Idle");
		gameObject.SetActive (false);
	}
}
