using UnityEngine;
using System.Collections;

public class CreditsBtn : MonoBehaviour
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
		yield return new WaitForSeconds (.5f);
		myAnim.Play ("Idle");
	}
}
