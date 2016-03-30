using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

	public GameObject P1ArrowClock;
	public GameObject P1ArrowAntiClock;
	public GameObject P2ArrowClock;
	public GameObject P2ArrowAntiClock;

	private bool P1ArrowClock_Done;
	private bool P1ArrowAntiClock_Done;
	private bool P2ArrowClock_Done;
	private bool P2ArrowAntiClock_Done;

	private float P1ClockTime = 2f;
	private float P1AntiClockTime = 2f;
	private float P2ClockTime = 2f;
	private float P2AntiClockTime = 2f;



	void Update ()
	{
		MobileControls ();
	}

	void MobileControls ()
	{
		int count = Input.touchCount;

		for (int i = 0; i < count; i++)
		{
			Touch touch = Input.GetTouch (i);

			if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
			{	
				MoveClockWise (OfflineManager.Instance.PlayerHolder1.transform);
				if (!P1ArrowAntiClock_Done)
				{
					P1AntiClockTime -= Time.deltaTime;

					if (P1AntiClockTime <= 0)
					{
						P1ArrowAntiClock_Done = true;
						P1ArrowAntiClock.SetActive (false);
					}
				}

			}

			if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
			{	
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder1.transform);
				if (!P1ArrowClock_Done)
				{
					P1ClockTime -= Time.deltaTime;
					if (P1ClockTime <= 0)
					{
						P1ArrowClock_Done = true;
						P1ArrowClock.SetActive (false);
					}
				}

			}

			if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
			{	
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder2.transform);
				if (!P2ArrowClock_Done)
				{
					P2ClockTime -= Time.deltaTime;
					if (P1ClockTime <= 0)
					{
						P2ArrowClock_Done = true;
						P2ArrowClock.SetActive (false);
					}
				}
			}

			if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
			{	
				MoveClockWise (OfflineManager.Instance.PlayerHolder2.transform);
				if (!P2ArrowAntiClock_Done)
				{
					P2AntiClockTime -= Time.deltaTime;

					if (P2AntiClockTime <= 0)
					{
						P2ArrowAntiClock_Done = true;
						P2ArrowAntiClock.SetActive (false);
					}
				}
			}
		}
	}

	void MoveClockWise (Transform t)
	{
		t.Rotate (0, 0, 5);
	}

	void MoveAntiClockWise (Transform t)
	{
		t.Rotate (0, 0, -5);
	}


}
