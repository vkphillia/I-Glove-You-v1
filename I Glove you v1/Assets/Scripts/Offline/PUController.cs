using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PUController : MonoBehaviour
{
	

	[HideInInspector]
	public PowerUp PU;
	[HideInInspector]

	public List<PowerUp> PUList = new List<PowerUp> ();

	private List<Vector3> spawnPointsArr = new List<Vector3> ();
	private List<Vector3> spawnPointsArrTemp = new List<Vector3> ();

	private float[] PUWeightTable = null;

	private GameObject Marker;

	void Awake ()
	{
		// Table to Store probability of PUS
		CreateWeightTable ();
		OfflineManager.SpwanFirstGlove += Spawn;
		PowerUp.OnPUPick += SpawnPU;
		OfflineRoundController.OnRoundOver += DestroyPU;
		for (int i = 0; i < PUList.Count; i++)
		{
			PUList [i].myAnim = PUList [i].GetComponent<Animator> ();
		}
		spawnPointsArr.Add (new Vector3 (-1.71f, 2f, 0));
		spawnPointsArr.Add (new Vector3 (-0.69f, -1.32f, 0));
		spawnPointsArr.Add (new Vector3 (-1.77f, -.21f, 0));
		spawnPointsArr.Add (new Vector3 (0f, 0, 0));
		spawnPointsArr.Add (new Vector3 (-0.3f, 1.05f, 0));
		spawnPointsArr.Add (new Vector3 (1.94f, 1.76f, 0));
		spawnPointsArr.Add (new Vector3 (0.72f, -2.44f, 0));
		spawnPointsArr.Add (new Vector3 (1.55f, -1.1f, 0));
		spawnPointsArr.Add (new Vector3 (-1.99f, -2.4f, 0));
		spawnPointsArr.Add (new Vector3 (0.4f, 2.4f, 0));

	}


	//spawn power ups code
	public IEnumerator SpawnPUCoroutine ()
	{
		int PUIndex = GetPUIndex ();
		PU = PUList [PUIndex];
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			yield return new WaitForSeconds (5f);
			PU.gameObject.SetActive (true);
			SpawnAnything (PU.gameObject);
		}
		else
		{
			yield return new WaitForSeconds (1f);
		}
		
		
	}

	//Helps pick PU based on it probability
	private int GetPUIndex ()
	{
		//generate a random number between 0 -1
		float t = Random.value;
		float q = 0.0f;
		
		for (int i = 0; i < PUList.Count; i++)
		{
			//increment q with the weight of the current PU
			q += PUWeightTable [i];
			if (t <= q)
			{
				return i;
			}
		}
		return 0;
	}

	private void CreateWeightTable ()
	{
		int noOfPU = PUList.Count;
		int i = 0;
		int sum = 0;
		
		//create a table of the length of the number of PU
		PUWeightTable = new float[noOfPU];

		for (i = 0; i < noOfPU; i++)
		{
			PowerUp PUScript = PUList [i];
		
			if (PUList != null)
			{
				sum += PUScript.weight;

				//store the weight in the weight table
				PUWeightTable [i] = (float)PUScript.weight;
			}
		}
		for (i = 0; i < noOfPU; i++)
		{
			PUWeightTable [i] /= sum;
		}
	}

	//glove
	void SpawnGlove ()
	{
		PUList [4].gameObject.SetActive (true);
		SpawnAnything (PUList [4].gameObject);
	}



	//for first glove triggered through event
	void Spawn ()
	{
		Invoke ("SpawnGlove", 4f);
	}


	public void SpawnStrikes (GameObject spawnObj)
	{
		


		int _randomPos = Random.Range (0, spawnPointsArr.Count);
		spawnObj.transform.position = spawnPointsArr [_randomPos];
		Debug.Log ("Strike=" + spawnPointsArr [_randomPos]);

		spawnPointsArrTemp.Add (spawnPointsArr [_randomPos]);
		spawnPointsArr.RemoveAt (_randomPos);
		if (spawnPointsArrTemp.Count > 0)
		{
			StartCoroutine (ReAddSpawnPoint ());
		}
	}



	public void SpawnAnything (GameObject spawnObj)
	{
		Vector3 _randomPos = new Vector3 (Random.Range (-1.7f, 1.7f), Random.Range (-2.4f, 2.4f), 0);
		StartCoroutine (spawnHighlight (_randomPos, spawnObj));
	}

	IEnumerator spawnHighlight (Vector3 _randomPos, GameObject spawnObj)
	{


		Marker = spawnObj.GetComponent<PowerUp> ().myMarker;
		Marker.SetActive (true);
		Marker.GetComponent<SpriteRenderer> ().color = spawnObj.GetComponent<PowerUp> ().myMarkerColor;
		Marker.transform.position = _randomPos;
		yield return new WaitForSeconds (.6f);
		Marker.SetActive (false);
		spawnObj.transform.position = _randomPos;
		//spawnPointsArrTemp.Add (spawnPointsArr [_randomPos]);
		//spawnPointsArr.RemoveAt (_randomPos);
	}

	//resize both spawn point array after every 5 seconds
	public IEnumerator ReAddSpawnPoint ()
	{
		yield return new WaitForSeconds (2f);
		spawnPointsArr.Add (spawnPointsArrTemp [0]);
		spawnPointsArrTemp.RemoveAt (0);
	}

	void SpawnPU ()
	{
		StartCoroutine (SpawnPUCoroutine ());
	}

	void DestroyPU ()
	{
		StopCoroutine (SpawnPUCoroutine ());
		PU.DeactivatePU (); 
	}

	void OnDestroy ()
	{
		OfflineManager.SpwanFirstGlove -= Spawn;
		PowerUp.OnPUPick -= SpawnPU;
		OfflineRoundController.OnRoundOver -= DestroyPU;

	}

}
