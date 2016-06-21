using UnityEngine;
using System.Collections;

public class Strike : MonoBehaviour
{

	public PUController myController;
	public Blast myChildBlast;
	private SpriteRenderer mySprite;
	public bool BlastActive;

	void Awake ()
	{
		myController = GameObject.FindObjectOfType<PUController> ();
		mySprite = GetComponent<SpriteRenderer> ();
	}

	void OnEnable ()
	{
		StartCoroutine (ActivateBlast ());
	}

	IEnumerator ActivateBlast ()
	{
		BlastActive = true;
		myController.SpawnStrikes (this.gameObject);
		yield return new WaitForSeconds (1.5f);
		//Debug.Log("Strike=" + transform.position);
		mySprite.enabled = true;
		yield return new WaitForSeconds (1f);
		myChildBlast.gameObject.SetActive (true);
		myChildBlast.myBlastAnim.Play ("blast_strike");
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("Blast_Strike", 0.2f);
		mySprite.enabled = false;
		yield return new WaitForSeconds (.5f);
		BlastActive = false;

	}



}