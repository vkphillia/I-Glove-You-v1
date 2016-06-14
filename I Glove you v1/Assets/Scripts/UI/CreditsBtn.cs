using UnityEngine;
using System.Collections;

public class CreditsBtn : MonoBehaviour
{

	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		StartCoroutine (ShowTitle ());
	}

	IEnumerator ShowTitle ()
	{
		myAnim.Play ("Appear");
		yield return new WaitForSeconds (.5f);
		myAnim.Play ("Idle");

	}
}
