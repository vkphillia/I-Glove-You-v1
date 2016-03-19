using UnityEngine;
using System.Collections;

public class BorderVerticle : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 10)
        {
			//other.gameObject.GetComponent<PlayerHolderController> ().hit = false;
			other.transform.Rotate (0, 0, (360 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
			//	StartCoroutine (other.gameObject.GetComponent<PlayerController> ().MakeHitFalse ());
			
			/*Vector3 force = transform.position - other.transform.position;
			force.Normalize ();
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (force * 300);*/
		}
	}
}
