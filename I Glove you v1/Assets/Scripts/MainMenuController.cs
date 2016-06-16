using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvas;
	
	public Transform backText;
    public GameObject spinningWheel;
	public GameObject PlayButton;
	public GameObject SettingsBtn;
    public GameObject title;

    private AsyncOperation async;
    private bool back;

    #region Instance
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
    #endregion
    
	void OnEnable ()
	{
		SoundsController.Instance.PlayBackgroundMusic (true, 0);//start BG music
		SoundsController.Instance.PlayBackgroundMusic (false, 1);//stop crowd sound
	}

    void Start()
    {
        spinningWheel.SetActive(true);
        Invoke("InitializeTitle", 0.6f);
    }

    void InitializeTitle()
    {
        title.SetActive(true);
        Invoke("InitializeMenu", 0.2f);
    }

    void InitializeMenu()
    {
        canvas.gameObject.SetActive(true);
    }

	public void PlayClick ()
	{
        ButtonClickSound();
        async = SceneManager.LoadSceneAsync("offline menu");
        async.allowSceneActivation = false;
        StartCoroutine (LoadingScene ("offline menu"));
	}

    public void ButtonClickSound()
    {
        SoundsController.Instance.PlayButtonClick();
    }

    public void MuteClick()
    {
        SoundsController.Instance.MuteSound();
    }

	IEnumerator LoadingScene (string sceneName)
	{
		float speed = 1;
		SoundsController.Instance.PlayBackgroundMusic (false, 0);//stop BG music
        
		while (canvas.alpha > 0)
		{
			canvas.alpha -= speed * Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds (0.5f);
        async.allowSceneActivation = true;
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
