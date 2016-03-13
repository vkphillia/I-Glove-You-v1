using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public Text signingResponse;

	//Static Singleton Instance
	public static MainMenuController _Instance = null;

	//property to get instance
	public static MainMenuController Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null) {
				_Instance = (MainMenuController)FindObjectOfType (typeof(MainMenuController));
			}
			return _Instance;
		}
	}

	void Start ()
	{
		MultiplayerController.Instance.TrySilentSignIn ();
	}

	public void Offline ()
	{
		SceneManager.LoadScene ("offline menu");
	}

	public void Online ()
	{
		MultiplayerController.Instance.SignInAndStartMPGame ();
		SceneManager.LoadScene ("online menu");
	}

	public void showSigninError ()
	{
		signingResponse.text = "sign in failed. Try later";
	}
}
