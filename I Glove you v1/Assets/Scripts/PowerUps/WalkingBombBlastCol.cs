using UnityEngine;
using System.Collections;

public delegate void BombHitEvent (PlayerHolderController p);

public class WalkingBombBlastCol : MonoBehaviour
{
	public static event BombHitEvent OnHit;

	private WalkingBombPU myParentBomb;
	[HideInInspector]
	public Collider2D myCol;
	[HideInInspector]
	public SpriteRenderer mySpriteRenderer;
	[HideInInspector]
	public Animator myAnim;

	void Awake ()
	{
		myParentBomb = GetComponentInParent<WalkingBombPU> ();
		myCol = GetComponent<Collider2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myAnim = GetComponent<Animator> ();

	}

	//if active bombs hits player with glove, reduce its health and swap gloves
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove) || other.gameObject.layer == 9)
		{
			//GetComponent<CircleCollider2D> ().enabled = false;
			//GetComponent<SpriteRenderer> ().enabled = false;
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder1));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder1));

			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentBomb.damageByBlast);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder1);
			}
			//StartCoroutine (OfflineManager.Instance.PlayerHolder1.MakeLyingDeadFalse ());

			//OfflineManager.Instance.PlayerHolder1.lyingDead = true;
			//OfflineManager.Instance.PlayerHolder1.myWalkAnim.Play ("P1Boxer_dead");
			//StartCoroutine (OfflineManager.Instance.PlayerHolder1.StartWalking ());



		}
		else if ((other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove) || other.gameObject.layer == 11)
		{
			//GetComponent<CircleCollider2D> ().enabled = false;
			//GetComponent<SpriteRenderer> ().enabled = false;
			StopCoroutine (myParentBomb.ActivateBomb (OfflineManager.Instance.PlayerHolder2));
			StartCoroutine (myParentBomb.BlastNow (OfflineManager.Instance.PlayerHolder2));

			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentBomb.damageByBlast);
			if (OnHit != null)
			{
				OnHit (OfflineManager.Instance.PlayerHolder2);
			}


			//StartCoroutine (OfflineManager.Instance.PlayerHolder2.MakeLyingDeadFalse ());
			//OfflineManager.Instance.PlayerHolder2.lyingDead = true;
			//OfflineManager.Instance.PlayerHolder2.myWalkAnim.Play ("Boxer_dead");



		}
	}

}
