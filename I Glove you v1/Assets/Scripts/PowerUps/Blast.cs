using UnityEngine;
using System.Collections;

public delegate void hitEvent (PlayerHolderController p);
public class Blast : MonoBehaviour
{
	public static event hitEvent OnHit;

	public AirStrikePU myParentPU;
	public Animator myBlastAnim;

	void Awake ()
	{
		myBlastAnim = GetComponent<Animator> ();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove) || other.gameObject.layer == 9)
		{
			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentPU.damage);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder1);
			}
		}
		else if ((other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove) || other.gameObject.layer == 11)
		{
			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentPU.damage);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder2);
			}
		}
	}
}
