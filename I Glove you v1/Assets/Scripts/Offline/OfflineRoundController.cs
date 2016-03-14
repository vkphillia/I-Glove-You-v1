using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OfflineRoundController : MonoBehaviour
{
	public Text myRoundText;


	public IEnumerator HideRoundStartText ()
	{
		OfflineManager.Instance.StartNewRound ();
		myRoundText.text = "Round " + OfflineManager.Instance.roundNumber;
		yield return new WaitForSeconds (3f);
		OfflineManager.Instance.currentState = GameState.Playing;
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{		
		myRoundText.text = "Round Over";
		yield return new WaitForSeconds (3f);
		StartCoroutine (HideRoundStartText ());
	}


	public IEnumerator HideMatchOverText ()
	{
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2) {
			myRoundText.text = "Player 1 Wins";
		} else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2) {
			myRoundText.text = "Player 2 Wins";
		}
		yield return new WaitForSeconds (3f);
		OfflineManager.Instance.StartNewRound ();
		SceneManager.LoadScene ("offline menu");
	}

	
	
	
}
