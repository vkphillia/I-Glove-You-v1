using UnityEngine;
using System.Collections;

public class SpeedDown : MonoBehaviour
{

	public float myTime;
	public float SpeedReduction;
	public int weight;

	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			StartCoroutine (SpeedBoost (OfflineManager.Instance.PlayerHolder2));
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			StartCoroutine (SpeedBoost (OfflineManager.Instance.PlayerHolder1));
		}
		else if (other.gameObject.layer == 9)
		{
			Debug.Log ("GloveHitSpeed");
			OfflineManager.Instance.PlayerHolder1.Punch ();
			DeactivatePU ();
		}
		else if (other.gameObject.layer == 11)
		{
			Debug.Log ("GloveHitSpeed");
			OfflineManager.Instance.PlayerHolder2.Punch ();
			DeactivatePU ();
		}
	}

	//reduce speed of the other player
	IEnumerator SpeedBoost (PlayerHolderController p)
	{
		//cannot deactivate gameobject as it will kill this coroutine so we disabled sprite and collider
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;

		p.mySpeed -= SpeedReduction;
		yield return new WaitForSeconds (myTime);
		p.mySpeed = OfflineManager.Instance.MaxSpeed;
		DeactivatePU ();
	}

	void DeactivatePU ()
	{
		//re-enabling th sprite and collider before deactivating gameObject
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
		OfflineManager.Instance.PUPicked = true;
		gameObject.SetActive (false);
	}


}
