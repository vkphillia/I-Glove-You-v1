using UnityEngine;
using System.Collections;

public class OfflineManager : MonoBehaviour
{
	//Static Singleton Instance
	public static OfflineManager _Instance = null;

	//property to get instance
	public static OfflineManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null) {                                                                                   
				_Instance = (OfflineManager)FindObjectOfType (typeof(OfflineManager));
			}
			return _Instance;
		}	
	}

	public Transform PlayerPrefab;
	public Transform PlayerTriggerPrefab;
	public Transform Foreground;
	public Vector3 Player1Pos;
	public Vector3 Player2Pos;
	public Transform PlayerHolder1;
	public Transform PlayerHolder2;

	
	[HideInInspector]
	public Transform Player1;
	[HideInInspector]
	public Transform Player2;
	[HideInInspector]
	public Transform Player1Trigger;
	[HideInInspector]
	public Transform Player2Trigger;

	void OnEnable ()
	{
		//Instantiate Player 1
		Player1 = Instantiate (PlayerPrefab, PlayerHolder1.transform.position, Quaternion.identity)as Transform;
		Player1.name = "Player1";
		Player1.SetParent (PlayerHolder1);
		Player1.GetComponent<SpriteRenderer> ().sprite = Player1.GetComponent<PlayerController> ().mySprites [0];
		Player1.gameObject.layer = 8;
		//Instantiate Player 1 Trigger
		Player1Trigger = Instantiate (PlayerTriggerPrefab, PlayerHolder1.transform.position, Quaternion.identity)as Transform;
		Player1Trigger.gameObject.layer = 9;
		Player1Trigger.SetParent (Player1);
		

		//Instantiate Player 2
		Player2 = Instantiate (PlayerPrefab, PlayerHolder2.transform.position, Quaternion.Euler (0, 0, 180))as Transform;
		Player2.name = "Player2";
		Player2.SetParent (PlayerHolder2);
		Player2.GetComponent<SpriteRenderer> ().sprite = Player2.GetComponent<PlayerController> ().mySprites [1];
		Player2.gameObject.layer = 10;
		//Instantiate Player 2 Trigger
		Player2Trigger = Instantiate (PlayerTriggerPrefab, PlayerHolder2.transform.position, Quaternion.Euler (0, 0, 180))as Transform;
		Player2Trigger.gameObject.layer = 11;
		Player2Trigger.SetParent (Player2);
	}
	
}
