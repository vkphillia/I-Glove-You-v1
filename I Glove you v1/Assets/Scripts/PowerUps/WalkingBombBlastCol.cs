using UnityEngine;
using System.Collections;

public class WalkingBombBlastCol : MonoBehaviour
{
	private WalkingBombPU myParentBomb;

	void Awake ()
	{
		myParentBomb = GetComponentInParent<WalkingBombPU> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder1));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder1));
		}
		else if (other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			Debug.Log ("Blast");
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder2));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder2));
		}
	}

}
