using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfflineRoundController : MonoBehaviour
{
	public Text myRoundText;
	public GameObject UI;

    //sets round start and over texts
    public void ShowRoundPanel()
    {
        gameObject.SetActive(true);

        if (OfflineManager.Instance.currentState == GameState.RoundStart)
        {
            StartCoroutine(HideRoundStartText());
        }
        else if (OfflineManager.Instance.currentState == GameState.RoundOver)
        {
            StartCoroutine(HideRoundOverText());
        }
        else if (OfflineManager.Instance.currentState == GameState.MatchOver)
        {
            StartCoroutine(HideMatchOverText());
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

		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_RoundStart);
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Fight);
		SoundsController.Instance.PlaySoundFX ("Round_Start");
        SoundsController.Instance.PlaySoundFX ("Fight");

		yield return new WaitForSeconds (1f);
		OfflineManager.Instance.currentState = GameState.Playing;
		gameObject.SetActive (false);
	}

	public IEnumerator HideRoundOverText ()
	{	
		myRoundText.text = "";
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
		if (OfflineManager.Instance.PlayerHolder1.roundWins == 2)
		{
			myRoundText.text = "Player 1 Wins";
		}
		else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
		{
			myRoundText.text = "Player 2 Wins";
		}
		yield return new WaitForSeconds (3f);
		myRoundText.text = "";
		OfflineManager.Instance.NewMatchStart ();
		UI.SetActive (true);
		//SceneManager.LoadScene ("offline menu");
	}

	IEnumerator RoundNumberSFX ()
	{
        //OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_Round);
        SoundsController.Instance.PlaySoundFX ("Round");
        
		yield return new WaitForSeconds (0.3f);
		//OfflineManager.Instance.PlaySound (OfflineManager.Instance.source_RoundNumber [OfflineManager.Instance.roundNumber - 1]);
		if (OfflineManager.Instance.roundNumber == 1)
            SoundsController.Instance.PlaySoundFX ("one");
		else if (OfflineManager.Instance.roundNumber == 2)
            SoundsController.Instance.PlaySoundFX ("two");
		else if (OfflineManager.Instance.roundNumber == 3)
            SoundsController.Instance.PlaySoundFX ("three");
	}
}
