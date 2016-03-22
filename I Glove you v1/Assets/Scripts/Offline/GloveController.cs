using UnityEngine;
using System.Collections;

public class GloveController : MonoBehaviour
{
	

	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == 8)
		{
			OfflineManager.Instance.PlayerHolder1.AddGlove ();
			OfflineManager.Instance.PlayerHolder2.LoseGlove ();

			OfflineManager.Instance.glovePicked = true;
			OfflineManager.Instance.glove.SetActive (false);
		}
		else if (other.gameObject.layer == 10)
		{
			OfflineManager.Instance.PlayerHolder1.LoseGlove ();
			OfflineManager.Instance.PlayerHolder2.AddGlove ();

			OfflineManager.Instance.glovePicked = true;
			OfflineManager.Instance.glove.SetActive (false);
		}
	}

	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
