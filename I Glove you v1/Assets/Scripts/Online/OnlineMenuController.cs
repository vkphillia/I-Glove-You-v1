using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnlineMenuController : MonoBehaviour
{

	public void Fight ()
	{
		SceneManager.LoadScene ("online game");
	}

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}
}
