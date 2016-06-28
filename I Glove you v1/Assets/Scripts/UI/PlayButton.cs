using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
	void Awake ()
	{
		
	}

	void OnEnable ()
	{
		//StartCoroutine (ShowTitle ());
	}

	IEnumerator ShowTitle ()
	{
        Animator myAnim;
        myAnim = GetComponent<Animator>();
        myAnim.Play ("Appear");
		yield return new WaitForSeconds (.3f);
		MainMenuController.Instance.SettingsBtn.SetActive (true);
		yield return new WaitForSeconds (.2f);
		myAnim.Play ("Idle");


	}
}
