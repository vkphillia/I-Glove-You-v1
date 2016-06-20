﻿using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
	public GameObject[] muteButton;
	public GameObject pausePanel;

	void Start ()
	{
		if (SoundsController.Instance != null)
		{
			if (SoundsController.mute)
			{
				//change mute object
				muteButton [0].SetActive (false);
				muteButton [1].SetActive (true);
			}
		}
	}

	void OnMouseUp ()
	{
		if (OfflineManager.Instance.currentState != GameState.Paused)
		{
			OfflineManager.Instance.currentState = GameState.Paused;
			StartCoroutine (pauseGame ());
		}

	}

	IEnumerator pauseGame ()
	{
		//gameObject.SetActive (false);
		pausePanel.SetActive (true);
		pausePanel.GetComponent<Animator> ().Play ("Appear");
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayBackgroundMusic (false, 0);//1 is for crowd sound
		yield return new WaitForSeconds (.5f);

		Time.timeScale = 0f;

	}

	public void OnResumeClick ()
	{
		if (OfflineManager.Instance.currentState == GameState.Paused)
		{
			Time.timeScale = 1f;
			OfflineManager.Instance.currentState = GameState.Playing;
			OfflineManager.Instance.pauseBtn.SetActive (true);
			if (SoundsController.Instance != null)
				SoundsController.Instance.PlayBackgroundMusic (true, 0);//1 is for crowd sound
			
		}


	}




	public void OnMuteClick ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.MuteSound ();
	}
}
