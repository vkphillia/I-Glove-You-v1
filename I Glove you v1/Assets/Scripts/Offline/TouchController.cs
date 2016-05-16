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
			}

			if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
			{	
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder1.transform);
			}

			if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
			{	
				MoveAntiClockWise (OfflineManager.Instance.PlayerHolder2.transform);
			}

			if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
			{	
				MoveClockWise (OfflineManager.Instance.PlayerHolder2.transform);
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
