using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PTrophy : PHUD
{
    #region variables
    public Image[] trophies1;
    public Image[] trophies2;

    private Animator myTrophyAnim;
    #endregion

    void Awake ()
	{
		myTrophyAnim = GetComponent<Animator> ();
	}
    
    void OnEnable ()
	{
        if(playerID==1)
        {
            transform.position = new Vector3(0, -1.6f, -1);
        }
        else
        {
            transform.position = new Vector3(0, 1.6f, -1);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
		StartCoroutine (MakeItFly ());
	}

    IEnumerator MakeItFly ()
	{
		yield return new WaitForSeconds (1f);
		myTrophyAnim.Play ("Trophy_Show");
		SoundsController.Instance.PlaySoundFX ("GlovePick", 0.15f);
		yield return new WaitForSeconds (1f);
        if(playerID==1)
        {
            StartCoroutine(SmoothPositionMovement.Instance.MoveGameObject(this.gameObject, new Vector3(0f, -4.3f, -1), 1f));
        }
        else
        {
            StartCoroutine(SmoothPositionMovement.Instance.MoveGameObject(this.gameObject, new Vector3(0f, 4.3f, -1), 1f));
        }
        yield return new WaitForSeconds(1f);
        DestoryGO();
        //iTween.MoveTo (this.gameObject, iTween.Hash ("position", new Vector3 (0f, -4.3f, -1), "time", 1f, "easetype", "linear", "onComplete", "DestoryGO"));

    }

	void DestoryGO ()
	{
		//OfflineManager.Instance.PlayerHolder1.myWinText_HUD.text = OfflineManager.Instance.PlayerHolder1.roundWins.ToString ();
        if(playerID==1)
        {
            if (OfflineManager.Instance.PlayerHolder1.roundWins == 1)
            {
                trophies1[0].color = Color.white;// SetActive(true);
            }
            else if (OfflineManager.Instance.PlayerHolder1.roundWins == 2)
            {
                trophies1[1].color = Color.white;
            }
        }
        else
        {
            if (OfflineManager.Instance.PlayerHolder2.roundWins == 1)
            {
                trophies2[0].color = Color.white;
            }
            else if (OfflineManager.Instance.PlayerHolder2.roundWins == 2)
            {
                trophies2[1].color = Color.white;
            }
        }
		SoundsController.Instance.PlaySoundFX ("CollectPoint", 0.15f);
		myTrophyAnim.Play ("Trophy_Idle");

		gameObject.SetActive (false);
	}
}
