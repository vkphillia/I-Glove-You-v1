using UnityEngine;
using System.Collections;

public class HealthPU : MonoBehaviour
{

	public int HealthUp;
	public int weight;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder1.AlterHealth (HealthUp);
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			OfflineManager.Instance.PlayerHolder2.AlterHealth (HealthUp);
		}
		else if (other.gameObject.layer == 9)
		{
			Debug.Log ("GloveHitHealth");
			OfflineManager.Instance.PlayerHolder1.Punch ();
		}
		else if (other.gameObject.layer == 11)
		{
			Debug.Log ("GloveHitHealth");
			OfflineManager.Instance.PlayerHolder2.Punch ();
		}
		DeactivatePU ();
	}

	void DeactivatePU ()
	{
		OfflineManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}
}
