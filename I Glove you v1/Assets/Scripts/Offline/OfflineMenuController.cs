using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{

	public static int Player1CharacterID;
	public static int Player2CharacterID;

    public CanvasGroup mainCanvas;
	public Button[] players1;
	public Button[] players2;
	public GameObject[] playerScrollViews;
    public GameObject[] selectedPlayers;

    public Text P1Text;
	public Text P2Text;

	private bool P1Ready;
	private bool P2Ready;

    

	void OnEnable ()
	{
        StartCoroutine(FadeCanvasElements(mainCanvas,0,1,0.3f));

		Player1CharacterID = 0;
		Player2CharacterID = 4;
		P1Text.text = "Fight";
		P2Text.text = "Fight";
		if (SoundsController.Instance != null)
		{
			SoundsController.Instance.PlayBackgroundMusic (false, 0);//stop BG music
			SoundsController.Instance.PlayBackgroundMusic (true, 1);//start crowd sound
		}

	}

    //this function works both as fade in and fade out for canvas elements with canvas group
    public IEnumerator FadeCanvasElements(CanvasGroup objectToFade, float fromAlpha, float toAlpha, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            objectToFade.alpha = Mathf.Lerp(fromAlpha, toAlpha, t / duration);
            yield return null;
        }
        if (objectToFade.alpha != toAlpha)
        {
            objectToFade.alpha = toAlpha;
        }
    }
    
	public void P1Fight ()
	{
        playerScrollViews[0].SetActive(false);
        selectedPlayers[0].GetComponent<Image>().sprite = players1[Player1CharacterID].image.sprite;
        selectedPlayers[0].SetActive(true);
        

		P1Ready = true;
		P1Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void P2Fight ()
	{
        playerScrollViews[1].SetActive(false);
        selectedPlayers[1].GetComponent<Image>().sprite = players2[Player2CharacterID].image.sprite;
        selectedPlayers[1].SetActive(true);

        P2Ready = true;
		P2Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void Player1Character (int id)
	{
		Player1CharacterID = id;
        players1[id].interactable = false;

        for (int i=0;i<5;i++)
        {
            if(i!=id)
                players1[i].interactable = true;
        }
	}

	public void Player2Character (int id)
	{
		Player2CharacterID = id;
        players2[id].interactable = false;
        
        for (int i = 0; i < 5; i++)
        {
            if (i != id)
                players2[i].interactable = true;
        }
    }

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}

	public void PlayBtnClick ()
	{
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

    void Update()
    {
        if (P1Ready && P2Ready)
        {
            P1Ready = false;
            P2Ready = false;
            Invoke("FadeOutElements", 0.6f);
            this.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    void FadeOutElements()
    {
        StartCoroutine(FadeCanvasElements(mainCanvas, 1, 0, 0.3f));
        Invoke("LoadGameScene", 0.3f);
    }

    void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("offline game");
    }
}
