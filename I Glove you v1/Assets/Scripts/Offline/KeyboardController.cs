using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class KeyboardController : MonoBehaviour
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


	private bool ZDown;
	private bool XDown;
	private bool NDown;
	private bool MDown;

	private int staminaKeYPresscounter;
	bool a;
	bool l;


	bool P1Rotate;
	bool P2Rotate;

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			
			KeyboardControls ();
			if (ZDown)
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
			else if (XDown)
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


			if (NDown)
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
			else if (MDown)
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

		}
		else
		{
			ZDown = false;
			XDown = false;
			NDown = false;
			MDown = false;
		}
		
	}


	void KeyboardControls ()
	{
		//PlayerPrefs 1		
		if (Input.GetKeyDown (KeyCode.Z))
		{
			ZDown = true;
			XDown = false;
			OfflineManager.Instance.PlayerHolder1.isTurning = true;
		}
		else if (Input.GetKeyDown (KeyCode.X))
		{
			XDown = true;	
			ZDown = false;		
			OfflineManager.Instance.PlayerHolder1.isTurning = true;
		}
		else if (Input.GetKeyUp (KeyCode.X))
		{
			XDown = false;
			OfflineManager.Instance.PlayerHolder1.isTurning = false;
		}
		else if (Input.GetKeyUp (KeyCode.Z))
		{
			ZDown = false;
			OfflineManager.Instance.PlayerHolder1.isTurning = false;
		}

		//player 2
		if (Input.GetKeyDown (KeyCode.N))
		{
			NDown = true;
			MDown = false;
			OfflineManager.Instance.PlayerHolder2.isTurning = true;
		}
		else if (Input.GetKeyDown (KeyCode.M))
		{
			MDown = true;	
			NDown = false;
			OfflineManager.Instance.PlayerHolder2.isTurning = true;
		}
		else if (Input.GetKeyUp (KeyCode.N))
		{
			NDown = false;
			OfflineManager.Instance.PlayerHolder2.isTurning = false;
		}
		else if (Input.GetKeyUp (KeyCode.M))
		{
			MDown = false;
			OfflineManager.Instance.PlayerHolder2.isTurning = false;
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
