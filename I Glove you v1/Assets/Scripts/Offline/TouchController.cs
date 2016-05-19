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

	private int staminaKeYPresscounter;


	private bool p1Touched;
	private bool p2Touched;


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

			if (touch.position.y < Screen.height / 2)
			{
				if (touch.position.x < Screen.width / 2)
				{
					MoveClockWise (OfflineManager.Instance.PlayerHolder1.transform);
				}
				else if (touch.position.x > Screen.width / 2)
				{
					MoveAntiClockWise (OfflineManager.Instance.PlayerHolder1.transform);
				}
				p1Touched = true;
			}

			if (touch.position.y > Screen.height / 2)
			{

				if (touch.position.x < Screen.width / 2)
				{
					MoveAntiClockWise (OfflineManager.Instance.PlayerHolder2.transform);
				}
				else if (touch.position.x > Screen.width / 2)
				{
					MoveClockWise (OfflineManager.Instance.PlayerHolder2.transform);
				}
				p2Touched = true;
			}
		}
		if (!p1Touched)
		{
			OfflineManager.Instance.PlayerHolder1.isTurning = false;
		}
		else
		{
			OfflineManager.Instance.PlayerHolder1.isTurning = true;
		}

		if (!p2Touched)
		{
			OfflineManager.Instance.PlayerHolder2.isTurning = false;
		}
		else
		{
			OfflineManager.Instance.PlayerHolder2.isTurning = true;
		}
		p1Touched = false;
		p2Touched = false;
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
