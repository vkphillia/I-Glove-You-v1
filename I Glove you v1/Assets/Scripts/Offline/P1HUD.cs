using UnityEngine;
using System.Collections;

public class P1HUD : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		myAnim.Play ("P1HUD_Up");
	}

	public IEnumerator GoDown ()
	{
		myAnim.Play ("P1HUD_Down");
		yield return new WaitForSeconds (0.75f);
		gameObject.SetActive (false);
	}
}