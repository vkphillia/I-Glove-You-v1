﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void RoundOverEvent ();
public class OfflineRoundController : MonoBehaviour
{

	public static event RoundOverEvent OnRoundOver;

	public Text myRoundText;
	public Text P1Text;
	public Text P2Text;
	//	public GameObject UI;
	private Animator myRoundTextAnim;
	public PHUD[] HUD;
	//public P2HUD HUDP2;
    

	void Awake ()
	{
		myRoundTextAnim = GetComponentInChildren<Animator> ();
	}

	//sets round start and over texts
	public void ShowRoundPanel ()
	{
		gameObject.SetActive (true);

		if (OfflineManager.Instance.currentState == GameState.RoundStart)
		{
			StartCoroutine (HideRoundStartText ());
		}
		else if (OfflineManager.Instance.currentState == GameState.RoundOver)
		{
			StartCoroutine (HideRoundOverText ());
		}
		else if (OfflineManager.Instance.currentState == GameState.MatchOver)
		{
			StartCoroutine (HideMatchOverText ());
		}
	}

	public IEnumerator HideRoundStartText ()
	{
		OfflineManager.Instance.StartNewRound ();
		if (OfflineManager.Instance.roundNumber == 1)
		{
			myRoundText.text = "Round I";
		}
		else if (OfflineManager.Instance.roundNumber == 2)
		{
			myRoundText.text = "Round II";
		}
		else if (OfflineManager.Instance.roundNumber == 3)
		{
			myRoundText.text = "Round III";
		}

		myRoundTextAnim.Play ("Round_Show");
		StartCoroutine (RoundNumberSFX ());
		yield return new WaitForSeconds (3f);
		//myRoundTextAnim.Play ("Round_Idle");
		myRoundText.text = "Fight!";
		OfflineManager.Instance.currentState = GameState.Fight;
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("Fight", 0.5f);
        
		yield return new WaitForSeconds (1f);
		myRoundTextAnim.Play ("Round_Hide");
		OfflineManager.Instance.currentState = GameState.Playing;
		OfflineManager.Instance.pauseBtn.SetActive (true);
		OfflineManager.Instance.pauseBtnCollider.gameObject.SetActive (true);
		//make player move
		OfflineManager.Instance.PlayerHolder1.myWalkAnim.Play ("WalkNoGlove");
		OfflineManager.Instance.PlayerHolder2.myWalkAnim.Play ("WalkNoGlove");
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayBackgroundMusic (true, 0);//BG Music
			StartCoroutine (SoundsController.Instance.FadeInOutBGMusic (0, 0f, .3f, 0.1f));//fade in
		}
			
			
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{	
		myRoundText.text = "";


		//destroy PU
		if (OnRoundOver != null)
		{
			OnRoundOver ();
		}

		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("RoundEnd", 0.5f);
		yield return new WaitForSeconds (.2f);
		myRoundText.text = "Round Over";
		myRoundTextAnim.Play ("Round_Show");

		yield return new WaitForSeconds (3f);
		myRoundTextAnim.Play ("Round_Hide");
		yield return new WaitForSeconds (1f);

		StartCoroutine (HideRoundStartText ());
	}

	//loads offline menu after showing the winner
	public IEnumerator HideMatchOverText ()
	{
		myRoundText.text = "Game Over";
		myRoundTextAnim.Play ("Round_Show");

		yield return new WaitForSeconds (1f);
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("Win", 0.5f);
		P1Text.gameObject.SetActive (true);
		P2Text.gameObject.SetActive (true);
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2)
		{
			


			if (OfflineManager.Instance.PlayerHolder1.myHealth == OfflineManager.Instance.MaxHealth)
			{
				P1Text.text = "You Win \n Flawless Victory!";
				P2Text.text = "You Lose";
			}
			else
			{
				P1Text.text = "You Win";
				P2Text.text = "You Lose";
			}

		}
		else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
		{

			if (OfflineManager.Instance.PlayerHolder2.myHealth == OfflineManager.Instance.MaxHealth)
			{
				P2Text.text = "You Win \n Flawless Victory!";
				P1Text.text = "You Lose";
			}
			else
			{
				P2Text.text = "You Win";
				P1Text.text = "You Lose";
			}
		}
		yield return new WaitForSeconds (5f);
		myRoundText.text = "";
		P1Text.gameObject.SetActive (false);
		P2Text.gameObject.SetActive (false);
		StartCoroutine (HUD [0].GoDown ());
		StartCoroutine (HUD [1].GoDown ());
		OfflineManager.Instance.NewMatchStart ();
		BackToOfflineMenu ();
		//UI.SetActive (true);

		//SceneManager.LoadScene ("offline menu");
	}

	IEnumerator RoundNumberSFX ()
	{
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Round);
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("Round", 0.5f);
        
		yield return new WaitForSeconds (0.3f);
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_RoundNumber [OfflineManager.Instance.roundNumber - 1]);
		if (SoundsController.Instance != null)
		{
			if (OfflineManager.Instance.roundNumber == 1)
				SoundsController.Instance.PlaySoundFX ("one", 0.5f);
			else if (OfflineManager.Instance.roundNumber == 2)
				SoundsController.Instance.PlaySoundFX ("two", 0.5f);
			else if (OfflineManager.Instance.roundNumber == 3)
				SoundsController.Instance.PlaySoundFX ("three", 0.5f);
		}
	}

	void BackToOfflineMenu ()
	{
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayBackgroundMusic (true, 1);//stop crowd sound
			SoundsController.Instance.PlayBackgroundMusic (false, 0);//start BG Music after loading next screen
		}
		SceneManager.LoadScene ("offline menu");
	}
}
