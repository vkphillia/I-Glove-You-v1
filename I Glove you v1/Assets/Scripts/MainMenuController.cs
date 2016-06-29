using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

	public enum MenuState
	{
		Menu,
		Help,
		Credits,
		Pay,
	}
;

	[SerializeField]
	private CanvasGroup menuPanel;
	
	public Transform backText;
	public GameObject spinningWheel;
	public GameObject PlayButton;
	public GameObject SettingsBtn;
	public GameObject title;
	public GameObject soundBtn;

	//manually closing panels
	public Animator menuPanelAnim;
	public Animator creditsPanelAnim;
	public Animator helpPanelAnim;
	public Animator payPanelAnim;

	//post purchase
	public GameObject InsertTxt;
	public GameObject Thanks;



	public MenuState currentState;

	private AsyncOperation async;
	private bool back;

	//iap
	public int currentPayStep;
	public Text[] IapTexts;


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
		currentState = MenuState.Menu;
		if (SoundsController.Instance != null)
		{
			//SoundsController.Instance.PlayBackgroundMusic (true, 0);//start BG music
			StartCoroutine (SoundsController.Instance.FadeInOutBGMusic (0, 0, 0.3f, .4f));//fade IN
			SoundsController.Instance.PlayBackgroundMusic (false, 1);//stop crowd sound
			if (SoundsController.mute)
			{
				soundBtn.transform.GetChild (1).gameObject.SetActive (false);
				soundBtn.transform.GetChild (0).gameObject.SetActive (true);
			}

		}
		spinningWheel.SetActive (true);
		Invoke ("InitializeTitle", 0.6f);
		StartCoroutine (SpiningWheel ());
		
	}

	void Start ()
	{
	}

	IEnumerator SpiningWheel ()
	{
	
		spinningWheel.transform.localScale = new Vector3 (0, 0, 0);
		float t = 0;
		while (spinningWheel.transform.localScale.x != 0.73f)
		{
			spinningWheel.transform.localScale = Vector3.MoveTowards (new Vector3 (0, 0, 0), new Vector3 (0.73f, 0.73f, 0.73f), t / 0.3f);
			yield return new WaitForEndOfFrame ();
			t += Time.deltaTime;
		}

		while (true)
		{
			Vector3 temp = spinningWheel.transform.eulerAngles;
			temp.z++;
			spinningWheel.transform.eulerAngles = temp;
			yield return new WaitForEndOfFrame ();
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
		menuPanel.GetComponent<Animator> ().Play ("Appear");
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

	public void PlayClick ()
	{
		ButtonClickSound ();
		//async = SceneManager.LoadSceneAsync ("offline menu");
		//async.allowSceneActivation = false;
		StartCoroutine (LoadingScene ("offline menu"));
	}

	IEnumerator LoadingScene (string sceneName)
	{
		//float speed = 1;
		//while (canvas.alpha > 0)
		//{
		//	canvas.alpha -= speed * Time.deltaTime;
		//	yield return null;
		//}
		menuPanel.GetComponentInChildren<Animator> ().Play ("Idle");
		menuPanel.GetComponent<Animator> ().Play ("Disappear");

		//SoundsController.Instance.PlayBackgroundMusic (false, 0);//stop BG music
		StartCoroutine (SoundsController.Instance.FadeInOutBGMusic (0, 0.3f, 0, 0.4f));//fade Out
		yield return new WaitForSeconds (0.7f);
		SceneManager.LoadScene ("offline menu");
		//async.allowSceneActivation = true;
	}

	void Update ()
	{
		if (currentState == MenuState.Menu)
		{
			if (Input.GetKeyDown (KeyCode.Escape) && back)
			{
				Application.Quit ();
				Debug.Log ("quit");
			}
		}


		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (currentState == MenuState.Menu)
			{
				back = true;
				backText.gameObject.SetActive (true);
				StartCoroutine (CloseBack ());


			}
			else if (currentState == MenuState.Help)
			{
				closeHelpPanel ();
			}
			else if (currentState == MenuState.Credits)
			{
				closeCreditsPanel ();
			}
			else if (currentState == MenuState.Pay)
			{
				closePayPanel ();
			}

		}
	}

	IEnumerator CloseBack ()
	{
		
		yield return new WaitForSeconds (1f);
		back = false;
		backText.gameObject.SetActive (false);

	}


	public void ChangeState (int stateNum)
	{
		if (stateNum == 0)
			currentState = MenuState.Menu;
		else if (stateNum == 1)
			currentState = MenuState.Help;
		else if (stateNum == 2)
			currentState = MenuState.Credits;
		else if (stateNum == 3)
			currentState = MenuState.Pay;
	}


	public void closeCreditsPanel ()
	{
		menuPanelAnim.Play ("Appear");
		creditsPanelAnim.Play ("SlideOut");
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayButtonClick ();
		}
		currentState = MenuState.Menu;
	}

	public void closeHelpPanel ()
	{
		menuPanelAnim.Play ("Appear");
		helpPanelAnim.Play ("SlideOut");
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayButtonClick ();
		}
		currentState = MenuState.Menu;
	}

	public void closePayPanel ()
	{
		menuPanelAnim.Play ("Appear");
		payPanelAnim.Play ("SlideOut");
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayButtonClick ();
		}
		currentState = MenuState.Menu;
	}

    
}
