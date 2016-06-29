using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuController : MonoBehaviour
{

	public static int Player1CharacterID;
	public static int Player2CharacterID;
	
	public Button[] players1;
	public Button[] players2;
	public GameObject[] playerScrollViews;
    public SpriteRenderer [] selectedPlayers;

    public Text P1Text;
	public Text P2Text;

	private bool P1Ready;
	private bool P2Ready;

    

	void OnEnable ()
	{
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


	void Update ()
	{
		if (P1Ready && P2Ready)
		{
            P1Ready = false;
			P2Ready = false;
            Invoke("LoadGameScene", 1f);
            this.enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Exit ();
		}
	}

	public void P1Fight ()
	{
        playerScrollViews[0].SetActive(false);
        selectedPlayers [0].sprite = players1 [Player1CharacterID].image.sprite;
		selectedPlayers [0].gameObject.SetActive (true);

		P1Ready = true;
		P1Text.text = "Ready!";
		if (SoundsController.Instance != null)
			SoundsController.Instance.PlayButtonClick ();//for button click sound
	}

	public void P2Fight ()
	{
        playerScrollViews[1].SetActive(false);
        selectedPlayers[1].sprite = players2[Player2CharacterID].image.sprite;
        selectedPlayers[1].gameObject.SetActive(true);

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

    void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("offline game");
    }
	//public void Selected (RectTransform gameobject)
	//{
	//	gameobject.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
	//}
}
