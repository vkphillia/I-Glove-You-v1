using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
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
		myAnim.Play ("Title_Appear");
		yield return new WaitForSeconds (.5f);
		myAnim.Play ("Title_Idle");

	}

}
