using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{

	public static int Player1CharacterID = 0;
	public static int Player2CharacterID = 4;
	public Text P1Text;
	public Text P2Text;

	public Sprite[] SelectedSprites;

	private bool P1Ready;
	private bool P2Ready;






	void OnEnable ()
	{
		//Player1CharacterID = 0;
		//Player2CharacterID = 5;
		P1Text.text = "Fight!";
		P2Text.text = "Fight!";
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
		P1Ready = true;
		P1Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void P2Fight ()
	{
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

	public void SelectSprite (int id)
	{

	}
}
