using UnityEngine;
using System.Collections;

public class payPanel : MonoBehaviour
{

	//int paySteps;


	void Start ()
	{
		//paySteps = 3;
		MainMenuController.Instance.currentPayStep = 0;
	}



	public void getCurrentPayStep (int paystep)
	{
		MainMenuController.Instance.currentPayStep = paystep;
		Debug.Log (MainMenuController.Instance.currentPayStep);
	}

	public void rate ()
	{
		#if UNITY_ANDROID
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.TheGoodSideGames.IGloveYou");
		#elif UNITY_IPHONE
		Application.OpenURL ("https://itunes.apple.com/us/app/i-glove-you/id1108847464?ls=1&mt=8");
		#endif

	}
}
