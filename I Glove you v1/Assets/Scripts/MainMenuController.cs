using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvas;
	private bool back;
	public Transform backText;
	public GameObject PlayButton;
	public GameObject SettingsBtn;



	//Static Singleton Instance
	public static MainMenuController _Instance = null;

	//property to get instance
	public static MainMenuController Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{
				_Instance = (MainMenuController)FindObjectOfType (typeof(MainMenuController));
			}
			return _Instance;
		}
	}


	private AsyncOperation async;
	public GameObject title;

	void OnEnable ()
	{
		title.SetActive (true);
	}

	public void Offline ()
	{
		//SceneManager.LoadScene ("offline menu");
		Debug.Log ("clicked");
		//async = SceneManager.LoadSceneAsync("offline menu");
		//async.allowSceneActivation = false;
		StartCoroutine (LoadingScene ("offline menu"));
	}


	IEnumerator LoadingScene (string sceneName)
	{
		float speed = 1;
        
		while (canvas.alpha > 0)
		{
			canvas.alpha -= speed * Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (sceneName);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			back = true;
			backText.gameObject.SetActive (true);
			StartCoroutine (ChkForDoubleBack ());
		}
	}

	IEnumerator ChkForDoubleBack ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && back)
		{
			Application.Quit ();
		}
		yield return new WaitForSeconds (.5f);
		back = false;
		backText.gameObject.SetActive (false);

	}


    
}
