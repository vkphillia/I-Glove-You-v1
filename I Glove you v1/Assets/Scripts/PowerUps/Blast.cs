using UnityEngine;
using System.Collections;

public delegate void hitEvent (PlayerHolderController p);
public class Blast : MonoBehaviour
{
	public static event hitEvent OnHit;

	public AirStrikePU myParentPU;

	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove) || other.gameObject.layer == 9)
		{
			Debug.Log ("GotBombed");
			//OfflineManager.Instance.PlayerHolder1.lyingDead = true;
			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentPU.damage);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder1);
			}
			//StartCoroutine (OfflineManager.Instance.PlayerHolder1.MakeLyingDeadFalse ());
			//Debug.Log ("makinglyingdeadfalse");

			//OfflineManager.Instance.PlayerHolder1.lyingDead = true;

			//OfflineManager.Instance.PlayerHolder1.myWalkAnim.Play ("Boxer_dead");
			//StartCoroutine (OfflineManager.Instance.PlayerHolder1.StartWalking ());

		}
		else if ((other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove) || other.gameObject.layer == 11)
		{
			Debug.Log ("GotBombed");

			//OfflineManager.Instance.PlayerHolder2.lyingDead = true;
			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentPU.damage);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder2);
			}
			//StartCoroutine (OfflineManager.Instance.PlayerHolder2.MakeLyingDeadFalse ());
			//Debug.Log ("makinglyingdeadfalse");

			//OfflineManager.Instance.PlayerHolder2.lyingDead = true;

			//OfflineManager.Instance.PlayerHolder2.myWalkAnim.Play ("Boxer_dead");
			//StartCoroutine (OfflineManager.Instance.PlayerHolder2.StartWalking ());

		}
	}
}
