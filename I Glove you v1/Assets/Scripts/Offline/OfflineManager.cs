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
	public Transform Foreground;
	public Vector3 Player1Pos;
	public Vector3 Player2Pos;
	public bool FirstTouch;

	[HideInInspector]
	public Transform Player1;
	[HideInInspector]
	public Transform Player2;

	



	void OnEnable ()
	{
		//Instantiate both Character and their sprites as per player selection
		Player1 = Instantiate (PlayerPrefab, Player1Pos, Quaternion.identity)as Transform;
		Player1.name = "Player1";
		Player1.SetParent (Foreground);
		Player1.GetComponent<SpriteRenderer> ().sprite = Player1.GetComponent<PlayerController> ().mySprites [0];
		Player2 = Instantiate (PlayerPrefab, Player2Pos, Quaternion.Euler (0, 0, 180))as Transform;
		Player2.name = "Player2";
		Player2.SetParent (Foreground);
		Player2.GetComponent<SpriteRenderer> ().sprite = Player2.GetComponent<PlayerController> ().mySprites [1];
	}
	
}
