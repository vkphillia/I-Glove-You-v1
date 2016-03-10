using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineMenuController : MonoBehaviour, MPLobbyListener
{
    public static string userName;
    public Text playerName;

    private bool _showLobbyDialog;
    private string _lobbyMessage;


    void Start()
    {
        playerName.text = userName;
    }

	public void Fight ()
	{
        if (!_showLobbyDialog)
        {
            //RetainedUserPicksScript.Instance.multiplayerGame = true;
            _lobbyMessage = "Starting a multi-player game...";
            _showLobbyDialog = true;
            MultiplayerController.Instance.lobbyListener = this;
            MultiplayerController.Instance.StartMatchMaking();
            //SceneManager.LoadScene ("online game");
        }

        //if (_showLobbyDialog)
        //{
        //    GUI.skin = guiSkin;
        //    GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), _lobbyMessage);
        //}

    }

	public void Exit ()
	{
		SceneManager.LoadScene ("main menu");
	}

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }

}
