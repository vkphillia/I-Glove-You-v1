using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour
{
	public AirStrikePU myParentPU;

	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.layer == 8 && OfflineManager.Instance.PlayerHolder1.hasGlove) || other.gameObject.layer == 9)
		{
			Debug.Log ("GotBombed");
			OfflineManager.Instance.PlayerHolder1.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder1.AlterHealth (myParentPU.damage);


		}
		else if ((other.gameObject.layer == 10 && OfflineManager.Instance.PlayerHolder2.hasGlove) || other.gameObject.layer == 11)
		{
			Debug.Log ("GotBombed");

			OfflineManager.Instance.PlayerHolder2.getPunched (this.transform);
			OfflineManager.Instance.PlayerHolder2.AlterHealth (myParentPU.damage);


		}
	}
}
