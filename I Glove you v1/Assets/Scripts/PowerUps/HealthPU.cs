using UnityEngine;
using System.Collections;

public class HealthPU : MonoBehaviour
{

	public int HealthUp;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			IncreaseHealth (OfflineManager.Instance.PlayerHolder1);
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			IncreaseHealth (OfflineManager.Instance.PlayerHolder2);
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

	void IncreaseHealth (PlayerHolderController p)
	{
		if ((p.myHealth + HealthUp) > OfflineManager.Instance.MaxHealth)
		{
			p.myHealth = OfflineManager.Instance.MaxHealth;
		}
		else
		{
			p.myHealth += HealthUp;
		}

		p.myHealthText_HUD.text = "Health " + p.myHealth;	
	}

	void DeactivatePU ()
	{
		OfflineManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}
}
