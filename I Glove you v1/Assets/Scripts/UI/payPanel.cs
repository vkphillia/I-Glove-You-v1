using UnityEngine;
using System.Collections;

public class payPanel : MonoBehaviour
{

	int paySteps;
	int currentPayStep;

	void Start ()
	{
		paySteps = 3;
		currentPayStep = 0;
	}

	public void OnLeftBtnClick ()
	{
		if (currentPayStep != 0)
		{
			currentPayStep--;
		}
	}

	public void OnRightBtnClick ()
	{
		if (currentPayStep != paySteps)
		{
			currentPayStep++;
		}
	}
}
