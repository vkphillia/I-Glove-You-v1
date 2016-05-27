using UnityEngine;
using System.Collections;

public class BorderVerticle : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
		{
			OfflineManager.Instance.PlayerHolder1.transform.Rotate (0, 0, (360 - OfflineManager.Instance.PlayerHolder1.transform.rotation.eulerAngles.z) - OfflineManager.Instance.PlayerHolder1.transform.rotation.eulerAngles.z);
		}
		if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
		{
			OfflineManager.Instance.PlayerHolder2.transform.Rotate (0, 0, (360 - OfflineManager.Instance.PlayerHolder2.transform.rotation.eulerAngles.z) - OfflineManager.Instance.PlayerHolder2.transform.rotation.eulerAngles.z);
		}
	}
}
