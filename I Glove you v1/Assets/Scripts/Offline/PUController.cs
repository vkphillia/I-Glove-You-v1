using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PUController : MonoBehaviour
{
	[HideInInspector]
	public GameObject PU;
	public List<GameObject> PUList = new List<GameObject> ();

	void Start ()
	{
		OfflineManager.Instance.PUPicked = true;
	}

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			if (OfflineManager.Instance.PUPicked)
			{
				StartCoroutine (SpawnPUCoroutine ());
			}
		}
		else
		{
			StopCoroutine (SpawnPUCoroutine ());
		}
	}

	//spawn power ups code
	public IEnumerator SpawnPUCoroutine ()
	{
		Debug.Log ("Coroutie started");
		OfflineManager.Instance.PUPicked = false;
		int randomChild = Random.Range (0, PUList.Count);
		PU = PUList [randomChild];
		yield return new WaitForSeconds (2f);
		PU.SetActive (true);
		PU.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0);
	}
}
