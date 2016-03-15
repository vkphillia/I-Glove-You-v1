using UnityEngine;
using System.Collections;

public class PUController : MonoBehaviour
{
	void OnEnable ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == 8) {
			OfflineManager.Instance.PUPicked = true;
			OfflineManager.Instance.PlayerHolder1.myHealth += 2;
			OfflineManager.Instance.PlayerHolder1.myHealthText_HUD.text = "Health " + OfflineManager.Instance.PlayerHolder1.myHealth;
		} else if (other.gameObject.layer == 10) {
			OfflineManager.Instance.PUPicked = true;
			OfflineManager.Instance.PlayerHolder2.myHealth += 2;
			OfflineManager.Instance.PlayerHolder2.myHealthText_HUD.text = "Health " + OfflineManager.Instance.PlayerHolder2.myHealth;
		}
		OfflineManager.Instance.PU.SetActive (false);
	}

	void OnDisable ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}
}
