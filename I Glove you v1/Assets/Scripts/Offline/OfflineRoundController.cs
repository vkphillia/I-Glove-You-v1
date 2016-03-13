using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OfflineRoundController : MonoBehaviour
{
	public Text myRoundText;


	void OnEnable ()
	{		
		StartCoroutine (HideRountText ());
	}

	IEnumerator HideRountText ()
	{
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2) {
			myRoundText.text = "Player 1 Wins";
		} else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2) {
			myRoundText.text = "Player 2 Wins";
		} else
			myRoundText.text = "Round " + OfflineManager.Instance.roundNumber;
		yield return new WaitForSeconds (3f);
		OfflineManager.Instance.currentState = GameState.Playing;
		gameObject.SetActive (false);
	}
}
