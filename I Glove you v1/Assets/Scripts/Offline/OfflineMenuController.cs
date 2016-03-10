using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class OfflineMenuController : MonoBehaviour
{

	private int Player1CharacterID;
	private int Player2CharacterID;

	//Change this method for player selection when ready until then auto select characters
	void Enable ()
	{
		Player1CharacterID = 0;
		Player2CharacterID = 1;
	}

	public void Fight ()
	{
		SceneManager.LoadScene ("offline game");
	}

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}
}
