using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{
	
	public GameObject P1Trigger;
	public GameObject P2Trigger;

	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == 8)
		{
			OfflineManager.Instance.PlayerHolder1.hasGlove = true;
			OfflineManager.Instance.PlayerHolder2.hasGlove = false;

			OfflineManager.Instance.glovePicked = true;
			P1Trigger.SetActive (true);
			P2Trigger.SetActive (false);
			OfflineManager.Instance.glove.SetActive (false);
		}
		else if (other.gameObject.layer == 10)
		{
			OfflineManager.Instance.PlayerHolder1.hasGlove = false;
			OfflineManager.Instance.PlayerHolder2.hasGlove = true;
			OfflineManager.Instance.glovePicked = true;
			P2Trigger.SetActive (true);
			P1Trigger.SetActive (false);
			OfflineManager.Instance.glove.SetActive (false);
		}
	}

	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
