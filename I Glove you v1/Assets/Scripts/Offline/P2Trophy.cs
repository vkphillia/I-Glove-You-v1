using UnityEngine;
using System.Collections;

public class P2Trophy : MonoBehaviour
{
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		transform.position = new Vector3 (0, 1, -1);
		myAnim.Play ("Trophy_Show");
		StartCoroutine (MakeItFly ());
	}

	IEnumerator MakeItFly ()
	{
		yield return new WaitForSeconds (1f);
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3 (2.3f, 4.5f, -1), "time", 2f, "easeInBounce", "linear", "onComplete", "DestoryGO"));

	}

	void DestoryGO ()
	{
		OfflineManager.Instance.PlayerHolder2.myWinText_HUD.text = OfflineManager.Instance.PlayerHolder2.roundWins.ToString ();
		//iTween.PunchScale (OfflineManager.Instance.PlayerHolder2.myWinText_HUD.GetComponentInParent<GameObject> (), iTween.Hash ("amount", new Vector3 (2f, 2f, 0), "time", .3f, "easetype", "linear"));

		gameObject.SetActive (false);
	}
}
