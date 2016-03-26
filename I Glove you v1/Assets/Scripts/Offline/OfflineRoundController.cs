using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfflineRoundController : MonoBehaviour
{
	public Text myRoundText;
	public Text P1Text;
	public Text P2Text;
	public GameObject UI;

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
		myRoundText.text = "Round " + OfflineManager.Instance.roundNumber;
		StartCoroutine (RoundNumberSFX ());
		yield return new WaitForSeconds (3f);
		myRoundText.text = "Fight!";
		OfflineManager.Instance.currentState = GameState.Fight;

		SoundsController.Instance.PlaySoundFX ("Fight", 1.0f);
        
		yield return new WaitForSeconds (1f);
		OfflineManager.Instance.currentState = GameState.Playing;
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{	
		myRoundText.text = "";
		SoundsController.Instance.PlaySoundFX ("RoundEnd", 1.0f);
		yield return new WaitForSeconds (1f);
		myRoundText.text = "Round Over";
		yield return new WaitForSeconds (3f);
		StartCoroutine (HideRoundStartText ());
	}

	//loads offline menu after showing the winner
	public IEnumerator HideMatchOverText ()
	{
		myRoundText.text = "";
		yield return new WaitForSeconds (2f);
		SoundsController.Instance.PlaySoundFX ("Win", 1.0f);
		P1Text.gameObject.SetActive (true);
		P2Text.gameObject.SetActive (true);
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2)
		{
			P1Text.text = "You Win";
			P2Text.text = "Lose Lose";
		}
		else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
		{
			P2Text.text = "You Win";
			P1Text.text = "You Lose";
		}
		yield return new WaitForSeconds (3f);
		myRoundText.text = "";
		P1Text.gameObject.SetActive (false);
		P2Text.gameObject.SetActive (false);
		OfflineManager.Instance.NewMatchStart ();
		UI.SetActive (true);
		//SceneManager.LoadScene ("offline menu");
	}

	IEnumerator RoundNumberSFX ()
	{
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Round);
		SoundsController.Instance.PlaySoundFX ("Round", 1.0f);
        
		yield return new WaitForSeconds (0.3f);
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_RoundNumber [OfflineManager.Instance.roundNumber - 1]);
		if (OfflineManager.Instance.roundNumber == 1)
			SoundsController.Instance.PlaySoundFX ("one", 1.0f);
		else if (OfflineManager.Instance.roundNumber == 2)
			SoundsController.Instance.PlaySoundFX ("two", 1.0f);
		else if (OfflineManager.Instance.roundNumber == 3)
			SoundsController.Instance.PlaySoundFX ("three", 1.0f);
	}
}
