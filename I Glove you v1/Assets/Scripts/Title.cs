using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
	public Animator[] sparkle;
	public GameObject[] paralaxGloves;



	void OnEnable ()
	{
		StartCoroutine (ShowTitle ());
		for (int i = 0; i < paralaxGloves.Length; i++)
		{
			paralaxGloves [i].SetActive (true);
		}
	}

	IEnumerator ShowTitle ()
	{
		//transform.localScale = new Vector3(0, 0, 0);
		//float t = 0;
		//while (transform.localScale.x != 1f)
		//{
		//    transform.localScale = Vector3.MoveTowards(new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f), t / 0.3f);
		//    yield return new WaitForEndOfFrame();
		//    t += Time.deltaTime;
		//}

		Animator myAnim;
		myAnim = GetComponent<Animator> ();
		myAnim.Play ("Title_Appear");
		yield return new WaitForSeconds (.5f);
		myAnim.Play ("Title_Idle");
		MainMenuController.Instance.PlayButton.SetActive (true);
		//play sparkle animation
		for (int i = 0; i < 3; i++)
		{
			sparkle [i].Play ("Shine");
			yield return new WaitForSeconds (.2f);
		}



	}



}
