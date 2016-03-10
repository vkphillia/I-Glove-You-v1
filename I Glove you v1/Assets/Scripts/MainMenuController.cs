using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	
	public void Offline ()
	{
		SceneManager.LoadScene ("offline menu");
	}

	public void Online ()
	{
		SceneManager.LoadScene ("online menu");
	}
}
