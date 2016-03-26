using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	
	//Static Singleton Instance
	public static MainMenuController _Instance = null;

	//property to get instance
	public static MainMenuController Instance
    {
		get
        {
			//if we do not have Instance yet
			if (_Instance == null)
            {
				_Instance = (MainMenuController)FindObjectOfType (typeof(MainMenuController));
			}
			return _Instance;
		}
	}
    
	public void Offline ()
	{
		SceneManager.LoadScene ("offline menu");
	}

    public void Story()
    {
        SceneManager.LoadScene("story main");
    }
    
}
