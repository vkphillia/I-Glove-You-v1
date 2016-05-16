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


	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			/*	if (!a)
			{
				OfflineManager.Instance.PlayerHolder1.transform.position += OfflineManager.Instance.PlayerHolder1.transform.up * Time.deltaTime * OfflineManager.Instance.PlayerHolder1.MaxSpeed;

			}
			else if (a)
			{
				OfflineManager.Instance.PlayerHolder1.transform.Rotate (0, 0, 5);
			
			}
			if (Input.GetKeyDown (KeyCode.A))
			{
				a = true;
			}
			else if (Input.GetKeyUp (KeyCode.A))
			{
				a = false;
			}


			if (!l)
			{
				OfflineManager.Instance.PlayerHolder2.transform.position += OfflineManager.Instance.PlayerHolder2.transform.up * Time.deltaTime * OfflineManager.Instance.PlayerHolder2.MaxSpeed;

			}
			else if (l)
			{
				OfflineManager.Instance.PlayerHolder2.transform.Rotate (0, 0, 5);
			
			}
			if (Input.GetKeyDown (KeyCode.L))
			{
				l = true;
			}
			else if (Input.GetKeyUp (KeyCode.L))
			{
				l = false;
			}
		}*/
			KeyboardControls ();
			RegainP1Stamina ();
			RegainP2Stamina ();
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
			else if (!XDown && !ZDown)
			{
				OfflineManager.Instance.PlayerHolder1.transform.position += OfflineManager.Instance.PlayerHolder1.transform.up * Time.deltaTime * OfflineManager.Instance.PlayerHolder1.MaxSpeed;
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
			else if (!NDown && !MDown)
			{
				OfflineManager.Instance.PlayerHolder2.transform.position += OfflineManager.Instance.PlayerHolder2.transform.up * Time.deltaTime * OfflineManager.Instance.PlayerHolder2.MaxSpeed;
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
		}
		else if (Input.GetKeyDown (KeyCode.X))
		{
			XDown = true;	
			ZDown = false;		
		}
		else if (Input.GetKeyUp (KeyCode.X))
		{
			XDown = false;
		}
		else if (Input.GetKeyUp (KeyCode.Z))
		{
			ZDown = false;
		}

		//player 2
		if (Input.GetKeyDown (KeyCode.N))
		{
			NDown = true;
			MDown = false;
		}
		else if (Input.GetKeyDown (KeyCode.M))
		{
			MDown = true;	
			NDown = false;		
		}
		else if (Input.GetKeyUp (KeyCode.N))
		{
			NDown = false;
		}
		else if (Input.GetKeyUp (KeyCode.M))
		{
			MDown = false;
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

	void RegainP2Stamina ()
	{
		if (OfflineManager.Instance.PlayerHolder2.waitingForStamina)
		{
			if (Input.GetKeyDown (KeyCode.N) || Input.GetKeyDown (KeyCode.M))
			{
				staminaKeYPresscounter++;
				Debug.Log ("staminaKeYPresscounter::" + staminaKeYPresscounter);
				if (staminaKeYPresscounter >= 5)
				{
					OfflineManager.Instance.PlayerHolder2.waitingForStamina = false;
					OfflineManager.Instance.PlayerHolder2.mySpeed = OfflineManager.Instance.PlayerHolder2.MaxSpeed;
					StopCoroutine (OfflineManager.Instance.PlayerHolder2.RegainSpeed ());
					staminaKeYPresscounter = 0;
				}
			}
		}
	}

	void RegainP1Stamina ()
	{
		if (OfflineManager.Instance.PlayerHolder1.waitingForStamina)
		{
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.X))
			{
				staminaKeYPresscounter++;
				Debug.Log ("staminaKeYPresscounter::" + staminaKeYPresscounter);

				if (staminaKeYPresscounter >= 5)
				{
					OfflineManager.Instance.PlayerHolder1.waitingForStamina = false;
					OfflineManager.Instance.PlayerHolder1.mySpeed = OfflineManager.Instance.PlayerHolder1.MaxSpeed;
					StopCoroutine (OfflineManager.Instance.PlayerHolder1.RegainSpeed ());
					staminaKeYPresscounter = 0;
				}
			}
		}
	}
}
