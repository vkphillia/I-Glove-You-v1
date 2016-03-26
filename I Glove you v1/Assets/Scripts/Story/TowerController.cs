﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TowerController : MonoBehaviour
{

	public List<Challenge> Challenge_List = new List<Challenge> ();


	void OnEnable ()
	{
		//PlayerData.Instance.ReadData ();
		//Debug.Log (PlayerData.Instance.Level);
		//for (int i = 0; i < Challenge_List.Count; i++)
		//{
		//	if (PlayerData.Instance.Level == i)
		//	{
		//		Debug.Log ("Player is on level " + Challenge_List [i].myLevelNum);
		//		Challenge_List [i].myButton.enabled = true;
		//		StoryManager.Instance.currentChallenge = Challenge_List [i];

		//	}
		//}
	}

	public void ClickOnChallenge ()
	{
		Debug.Log ("Starting Challenge - " + StoryManager.Instance.currentChallenge.myLevelDesciption);
		StoryManager.Instance.currentState = StoryGameState.RoundStart;
		StoryManager.Instance.RoundPanel.gameObject.SetActive (true);
		StoryManager.Instance.ChallengeTowerPanel.SetActive (false);


	}

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        //SceneManager.LoadSceneAsync("Tutorial");
        //StartCoroutine(LoadingScene());
    }
    IEnumerator LoadingScene()
    {
        while(!SceneManager.LoadSceneAsync("Tutorial").isDone)
        {
            Debug.Log(SceneManager.LoadSceneAsync("Tutorial").progress);
            yield return new WaitForSeconds(0.1f);
            
        }
        Debug.Log(SceneManager.LoadSceneAsync("Tutorial").isDone);
    }

    public void ChallengeOne()
    {
        SceneManager.LoadScene("1st challenge");
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("main menu");
    }
}
