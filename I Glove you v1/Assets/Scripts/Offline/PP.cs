using UnityEngine;
using System.Collections;

public class PP : MonoBehaviour
{

	void OnEnable ()
	{
		StartCoroutine ("RemovePP");
	}

	IEnumerator RemovePP ()
	{
		yield return new WaitForSeconds (4f);
		GameObjectPool.GetPool ("PPPool").ReleaseInstance (transform);
	}


	public virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 && !OfflineManager.Instance.PlayerHolder1.hasGlove)
		{
			Debug.Log ("Player1PPcollected");
			OfflineManager.Instance.PlayerHolder1.UpdatePP ();
			GameObjectPool.GetPool ("PPPool").ReleaseInstance (transform);
		}
		else if (other.gameObject.layer == 10 && !OfflineManager.Instance.PlayerHolder2.hasGlove)
		{
			Debug.Log ("Player2PPcollected");
			OfflineManager.Instance.PlayerHolder2.UpdatePP ();
			GameObjectPool.GetPool ("PPPool").ReleaseInstance (transform);
		}
		else if (other.gameObject.layer == 9)
		{
			Debug.Log ("Player1PPdestroyed");
			OfflineManager.Instance.PlayerHolder1.PunchPUS (this.transform);
			GameObjectPool.GetPool ("PPPool").ReleaseInstance (transform);
		}
		else if (other.gameObject.layer == 11)
		{
			Debug.Log ("Player2PPdestroyed");
			OfflineManager.Instance.PlayerHolder2.PunchPUS (this.transform);
			GameObjectPool.GetPool ("PPPool").ReleaseInstance (transform);
		}
	}




}
