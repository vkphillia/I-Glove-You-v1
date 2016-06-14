using UnityEngine;
using System.Collections;

public class HelpBtn : MonoBehaviour
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
		yield return new WaitForSeconds (.3f);
		MainMenuController.Instance.MuteBtn.SetActive (true);
		yield return new WaitForSeconds (.2f);
		myAnim.Play ("Idle");

	}
}
