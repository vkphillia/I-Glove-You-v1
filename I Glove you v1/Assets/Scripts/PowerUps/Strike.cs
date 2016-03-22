using UnityEngine;
using System.Collections;

public class Strike : MonoBehaviour
{

	
	public Transform myChildBlast;

	void OnEnable ()
	{
		StartCoroutine (ActivateBlast ());
	}


	IEnumerator ActivateBlast ()
	{
		transform.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3, 3), 0);
		yield return new WaitForSeconds (0.75f);
		GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (1f);
		myChildBlast.gameObject.SetActive (true);
		myChildBlast.GetComponent<Animator> ().Play ("blast_strike");
		SoundsController.Instance.PlaySoundFX ("Blast");
	}
}