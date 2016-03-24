using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{

	public void Tower ()
	{
		StoryManager.Instance.currentState = StoryGameState.ChallengeSelect;
		StoryManager.Instance.ChallengeTowerPanel.SetActive (true);
		PlayerData.Instance.Level++;
		PlayerData.Instance.SaveData ();
		gameObject.SetActive (false);
	}
}
