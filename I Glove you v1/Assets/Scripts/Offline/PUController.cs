using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PUController : MonoBehaviour
{
	[HideInInspector]
	public GameObject PU;
	public List<GameObject> PUList = new List<GameObject> ();
	//PU spawning
	private float[] PUWeightTable = null;



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
		OfflineManager.Instance.PUPicked = false;
		int randomChild = Random.Range (0, PUList.Count);
		PU = PUList [randomChild];
		yield return new WaitForSeconds (2f);
		PU.SetActive (true);
		PU.transform.position = new Vector3 (Random.Range (-2f, 2f), Random.Range (-3f, 3f), 0);
	}


	/*private void CreateWeightTable ()
	{
		int noOfPU = PUList.Count;
		int i = 0;
		int sum = 0;
		
		//create a table of the length of the number of PU
		PUWeightTable = new float[noOfPU];

		for (i = 0; i < noOfPU; i++)
		{
			//get component
			DestructableItem bonusScript = bonusPrefabs [i].GetComponent<DestructableItem> ();
		
			if (PUList != null)
			{
				sum += bonusScript.weight;

				//store the weight in the weight table
				BonusWeightTable [i] = (float)bonusScript.weight;
			}
		}
		for (i = 0; i < noOfBonus; i++)
		{
			BonusWeightTable [i] /= sum;

		}


	}

	private int getNextPUndex ()
	{
		//generate a random number between 0 -1
		float t = Random.value;
		float q = 0.0f;

		for (int i = 0; i < PUList.Count; i++)
		{
			//increment q with the weight of the current brick
			q += PUWeightTable [i];
			if (t <= q)
			{
				return i;
			}
		}
		return 0;
	}*/


}
