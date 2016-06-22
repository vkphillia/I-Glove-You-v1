using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{

	public static int Player1CharacterID = 0;
	public static int Player2CharacterID = 4;
    public SwipePlayer[] playerSelectionHolder;

    public Text P1Text;
	public Text P2Text;

	private bool P1Ready;
	private bool P2Ready;

    

	void OnEnable ()
	{
		Player1CharacterID = 0;
		Player2CharacterID = 4;
		P1Text.text = "Fight";
		P2Text.text = "Fight";
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayBackgroundMusic (false, 0);//stop BG music
			SoundsController.Instance.PlayBackgroundMusic (true, 1);//start crowd sound
		}

	}


	void Update ()
	{
		if (P1Ready && P2Ready)
		{
			P1Ready = false;
			P2Ready = false;
			SceneManager.LoadSceneAsync ("offline game");
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Exit ();
		}
	}

	public void P1Fight ()
	{
        Player1CharacterID = playerSelectionHolder[0].selectedID[1];// setting player ID

        P1Ready = true;
		P1Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void P2Fight ()
	{
        Player2CharacterID = playerSelectionHolder[1].selectedID[1];// setting player ID

        P2Ready = true;
		P2Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void Player1Character (int id)
	{
		Player1CharacterID = id;
	}

	public void Player2Character (int id)
	{
		Player2CharacterID = id;
	}

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}

	public void PlayBtnClick ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

    public void Selected(RectTransform gameobject)
    {
        gameobject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
    }
}
