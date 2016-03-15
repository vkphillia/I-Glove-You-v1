using UnityEngine;
using System.Collections;

public class BorderHorizontal : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 10) {
			//other.gameObject.GetComponent<PlayerHolderController> ().hit = false;
			other.transform.Rotate (0, 0, (180 - other.transform.localRotation.eulerAngles.z) - other.transform.localRotation.eulerAngles.z);
			//other.gameObject.GetComponent<PlayerController> ().hit = true;
			//StartCoroutine (other.gameObject.GetComponent<PlayerController> ().MakeHitFalse ());
			/*Vector3 force = other.transform.position - transform.position;
			force.Normalize ();
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (force * 300);*/
		}

	}


}
