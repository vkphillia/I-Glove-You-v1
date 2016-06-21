using UnityEngine;
using System.Collections;

public class PHUD : MonoBehaviour
{
	public int playerID;

	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		StartCoroutine (ShowHUD ());
	}

	public IEnumerator ShowHUD ()
	{
		if (playerID == 1)
		{
			myAnim.Play ("P1HUD_Up");
		}
		else
		{
			myAnim.Play ("P2HUD_Up");
		}
		yield return new WaitForSeconds (0.6f);
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlaySoundFX ("BoxingBell", 0.3f);
	}

	public IEnumerator GoDown ()
	{
		if (playerID == 1)
		{
			myAnim.Play ("P1HUD_Down");
		}
		else
		{
			myAnim.Play ("P2HUD_Down");
		}
		yield return new WaitForSeconds (0.75f);
		gameObject.SetActive (false);
	}
}