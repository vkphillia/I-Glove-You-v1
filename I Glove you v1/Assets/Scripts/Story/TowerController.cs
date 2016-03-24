using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{

	public List<Challenge> Challenge_List = new List<Challenge> ();


	void OnEnable ()
	{
		for (int i = 0; i < Challenge_List.Count; i++)
		{
			if (PlayerData.Instance.Level == i)
			{
				Debug.Log ("Player is on level " + Challenge_List [i].myLevelNum);
				Challenge_List [i].myButton.enabled = true;
				StoryManager.Instance.currentChallenge = Challenge_List [i];

			}
		}
	}

	public void ClickOnChallenge ()
	{
		Debug.Log ("Starting Challenge - " + StoryManager.Instance.currentChallenge.myLevelDesciption);
		StoryManager.Instance.currentState = StoryGameState.RoundStart;
		StoryManager.Instance.RoundPanel.gameObject.SetActive (true);
		StoryManager.Instance.ChallengeTowerPanel.SetActive (false);


	}
}
