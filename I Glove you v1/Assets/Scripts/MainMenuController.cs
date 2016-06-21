using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup menuPanel;
	
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
		if (SoundsController.Instance != null)
		{
			//SoundsController.Instance.PlayBackgroundMusic (true, 0);//start BG music
            StartCoroutine(SoundsController.Instance.FadeInOutBGMusic(0,0, 0.3f, 1f));//fade IN
			SoundsController.Instance.PlayBackgroundMusic (false, 1);//stop crowd sound
		}
		
	}

	void Start ()
	{
		spinningWheel.SetActive (true);
        Invoke ("InitializeTitle", 0.6f);
        StartCoroutine(SpiningWheel());
	}

    IEnumerator SpiningWheel()
    {
        spinningWheel.transform.localScale = new Vector3(0, 0, 0);
        float t = 0;
        while (spinningWheel.transform.localScale.x != 0.73f)
        {
            spinningWheel.transform.localScale = Vector3.MoveTowards(new Vector3(0, 0, 0), new Vector3(0.73f, 0.73f, 0.73f), t / 0.3f);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }

        while(true)
        {
            Vector3 temp = spinningWheel.transform.eulerAngles;
            temp.z++;
            spinningWheel.transform.eulerAngles = temp;
            yield return new WaitForEndOfFrame();
        }
    }

	void InitializeTitle ()
	{
		title.SetActive (true);
		Invoke ("InitializeMenu", 0.5f);
	}

	void InitializeMenu ()
	{
		menuPanel.gameObject.SetActive (true);
        menuPanel.GetComponent<Animator>().Play("Appear");
        //StartCoroutine(SmoothPositionMovement.Instance.MoveCanvasElement(menuPanel.GetComponent<RectTransform>(), Vector3.zero, 0.2f));
	}

	public void ButtonClickSound ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();
	}

	public void MuteClick ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.MuteSound ();
	}

    public void PlayClick()
    {
        ButtonClickSound();
        //async = SceneManager.LoadSceneAsync ("offline menu");
        //async.allowSceneActivation = false;
        StartCoroutine(LoadingScene("offline menu"));
    }

    IEnumerator LoadingScene (string sceneName)
	{
        //float speed = 1;
        //while (canvas.alpha > 0)
        //{
        //	canvas.alpha -= speed * Time.deltaTime;
        //	yield return null;
        //}
        menuPanel.GetComponentInChildren<Animator>().Play("Idle");
        menuPanel.GetComponent<Animator>().Play("Disappear");

        //SoundsController.Instance.PlayBackgroundMusic(false, 0);//stop BG music
        StartCoroutine(SoundsController.Instance.FadeInOutBGMusic(0,0.3f,0,0.8f));//fade Out
        yield return new WaitForSeconds (0.7f);
        SceneManager.LoadScene("offline menu");
        //async.allowSceneActivation = true;
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
