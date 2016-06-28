using UnityEngine;
using System.Collections;

public class payPanel : MonoBehaviour
{

	//int paySteps;
	public int currentPayStep;

	void Start ()
	{
		//paySteps = 3;
		currentPayStep = 0;
	}



	public void getCurrentPayStep (int paystep)
	{
		currentPayStep = paystep;
	}

	public void rate ()
	{
		#if UNITY_ANDROID
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=com.TheGoodSideGames.Wordifly");
		#elif UNITY_IPHONE
		Application.OpenURL ("https://itunes.apple.com/us/app/i-glove-you/id1108847464?ls=1&mt=8");
		#endif

	}
}
