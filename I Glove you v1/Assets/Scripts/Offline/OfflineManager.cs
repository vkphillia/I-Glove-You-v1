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

	
	public Transform PlayerHolder1;
	public Transform PlayerHolder2;
	public Transform Player1;
	public Transform Player2;
	public GameObject glove;
	public bool glovePicked;

	void OnEnable ()
	{
		Player1.GetComponent<SpriteRenderer> ().sprite = Player1.GetComponent<OfflinePlayerController> ().mySprites [0];
		Player2.GetComponent<SpriteRenderer> ().sprite = Player2.GetComponent<OfflinePlayerController> ().mySprites [1];
	}

	void Start ()
	{
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4f, 4f), 0);
	}

	void Update ()
	{
		if (glovePicked) {
			StartCoroutine (SpawnGlove ());

		}
	}

	public IEnumerator SpawnGlove ()
	{
		glovePicked = false;
		Debug.Log ("Spawned");
		yield return new WaitForSeconds (5f);
		glove.SetActive (true);
		glove.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-4f, 4f), 0);
		
	}

	
}
