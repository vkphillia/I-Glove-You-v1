using UnityEngine;
using System.Collections;

public class OnlineManager : MonoBehaviour
{

	//Static Singleton Instance
	public static OnlineManager _Instance = null;

	//property to get instance
	public static OnlineManager Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null) {                                                                                   
				_Instance = (OnlineManager)FindObjectOfType (typeof(OnlineManager));
			}
			return _Instance;
		}	
	}

	public Transform PlayerPrefab;
	public Transform Foreground;

	void SpawnPlayers ()
	{
		//Instantiate both Character, their sprites as per player selection and set parent as foreground
	}
}
