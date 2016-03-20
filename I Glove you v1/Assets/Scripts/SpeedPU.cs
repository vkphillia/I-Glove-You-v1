using UnityEngine;
using System.Collections;

public class SpeedPU : MonoBehaviour
{

	public float myTime;
	public float SpeedUp;

	void Update ()
	{
		
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == 8)
		{
			if (!OfflineManager.Instance.PlayerHolder1.hasGlove)
			{
				OfflineManager.Instance.PUPicked = true;
				StartCoroutine (SpeedBoost (OfflineManager.Instance.PlayerHolder1));

			}
		}
		else if (other.gameObject.layer == 10)
		{
			if (!OfflineManager.Instance.PlayerHolder1.hasGlove)
			{
				OfflineManager.Instance.PUPicked = true;
				StartCoroutine (SpeedBoost (OfflineManager.Instance.PlayerHolder2));
			}
			
		}

		gameObject.SetActive (false);
	}

	IEnumerator SpeedBoost (PlayerHolderController p)
	{
		p.mySpeed += SpeedUp;
		yield return new WaitForSeconds (myTime);
		p.mySpeed = OfflineManager.Instance.MaxSpeed;
	}


}
