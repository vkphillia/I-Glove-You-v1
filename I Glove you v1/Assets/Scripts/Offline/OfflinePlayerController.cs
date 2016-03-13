﻿using UnityEngine;
using System.Collections;

public class OfflinePlayerController : MonoBehaviour
{
	public Sprite[] mySprites;
	public Animator myPunchAnim;

	void Awake ()
	{
		//myPunchAnim = GetComponentInChildren<Animator> ();
		PlayerHolderController.OnTrigger += Punch;
	}

	IEnumerator PlayPunchAnim ()
	{
		myPunchAnim.Play ("Punch_Hit");
		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}


	void Punch ()
	{
		StartCoroutine (PlayPunchAnim ());
	}

	void OnDestroy ()
	{
		PlayerHolderController.OnTrigger -= Punch;
	}
}
