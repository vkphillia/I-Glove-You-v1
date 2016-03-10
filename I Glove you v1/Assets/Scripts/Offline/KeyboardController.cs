using UnityEngine;
using System.Collections;


public class KeyboardController : MonoBehaviour
{

	private bool ZDown;
	private bool XDown;
	private bool NDown;
	private bool MDown;

	void Update ()
	{
		KeyboardControls ();
		if (ZDown) {
			MoveClockWise (OfflineManager.Instance.Player1);
		} else if (XDown) {
			MoveAntiClockWise (OfflineManager.Instance.Player1);
		}
		if (NDown) {
			MoveClockWise (OfflineManager.Instance.Player2);
		} else if (MDown) {
			MoveAntiClockWise (OfflineManager.Instance.Player2);
		}
	}


	void KeyboardControls ()
	{
		//PlayerPrefs 1		
		if (Input.GetKeyDown (KeyCode.Z)) {
			ZDown = true;
			XDown = false;
			
			

		} else if (Input.GetKeyDown (KeyCode.X)) {
			XDown = true;	
			ZDown = false;	
			
			
		} else if (Input.GetKeyUp (KeyCode.X)) {
			XDown = false;
		} else if (Input.GetKeyUp (KeyCode.Z)) {
			ZDown = false;
		}

		//player 2
		if (Input.GetKeyDown (KeyCode.N)) {
			NDown = true;
			MDown = false;
			
			

		} else if (Input.GetKeyDown (KeyCode.M)) {
			MDown = true;	
			NDown = false;	
			
			
		} else if (Input.GetKeyUp (KeyCode.N)) {
			NDown = false;
		} else if (Input.GetKeyUp (KeyCode.M)) {
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
}
