using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{

	
	void Update ()
	{
		MobileControls ();
	}

	

	void MobileControls ()
	{
		int count = Input.touchCount;
		for (int i = 0; i < count; i++) {
			Touch touch = Input.GetTouch (i);

			if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2) {
				
				MoveClockWise (OfflineManager.Instance.Player1);
			}
			if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2) {
				
				MoveAntiClockWise (OfflineManager.Instance.Player1);
			}
			if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2) {
				
				MoveAntiClockWise (OfflineManager.Instance.Player2);
			}
			if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2) {
				
				MoveClockWise (OfflineManager.Instance.Player2);
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
