using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
	public GameObject[] muteButton;

	void Start ()
	{
		if (SoundsController.mute)
		{
			//change mute object
			muteButton [0].SetActive (false);
			muteButton [1].SetActive (true);
		}
	}

	public void OnPauseBtn ()
	{
		/*if (!Pause)
		{
			Pause = true;Playing

			Time.timeScale = 0;

				
		}
		else
		{
			Pause = false;
			Time.timeScale = 1;

		}*/
		if (OfflineManager.Instance.currentState == GameState.Paused)
		{
			OfflineManager.Instance.currentState = GameState.Playing;
			OfflineManager.Instance.pauseBtn.SetActive (true);
			SoundsController.Instance.PlayBackgroundMusic (true, 0);//1 is for crowd sound
		}
		else if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			OfflineManager.Instance.currentState = GameState.Paused;
			SoundsController.Instance.PlayBackgroundMusic (false, 0);//1 is for crowd sound
		}
	}

	public void OnMuteClick ()
	{
		SoundsController.Instance.MuteSound ();
	}
}
