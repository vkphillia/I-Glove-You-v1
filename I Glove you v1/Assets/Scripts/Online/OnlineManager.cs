using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.UI;

public class OnlineManager : MonoBehaviour, MPUpdateListener, MPLobbyListener
{
    
    public Text debugText;
    public Text debugMessagesLobbby;
    //player and opponent prefabs
    public GameObject PlayerPrefab;
    public GameObject opponentPrefab;
    public Transform Foreground;

    private GameObject playerCopy;
    

    //lobby messages
    private string _lobbyMessage;
    private bool _showLobbyDialog;
    
    //network traffic
    private float _nextBroadcastTime = 0;

    //multiplayer stuffs
    private bool _multiplayerReady;
    private string _myParticipantId;
    private Vector2 _startingPoint = new Vector2(0f, -4.0f);
    private float _startingPointYOffset = 8f;
    private Dictionary<string, OpponentController> _opponentScripts;

    //Static Singleton Instance
    public static OnlineManager _Instance = null;

	//property to get instance
	public static OnlineManager Instance
    {
		get
        {
			//if we do not have Instance yet
			if (_Instance == null)
            {                                                                                   
				_Instance = (OnlineManager)FindObjectOfType (typeof(OnlineManager));
			}
			return _Instance;
		}	
	}
    
    void Start()
    {
        SetupMultiplayerGame();
        _showLobbyDialog = true;
    }
    
    //player and opponenet spawn and sprite selection done here
    void SetupMultiplayerGame()
    {
        MultiplayerController.Instance.updateListener = this;
        _myParticipantId = MultiplayerController.Instance.GetMyParticipantId();
        // 2
        List<Participant> allPlayers = MultiplayerController.Instance.GetAllPlayers();
        _opponentScripts = new Dictionary<string, OpponentController>(allPlayers.Count - 1);

        for (int i = 0; i < allPlayers.Count; i++)
        {
            string nextParticipantId = allPlayers[i].ParticipantId;
            //Debug.Log("Setting up car for " + nextParticipantId);
            // 3
            Vector3 carStartPoint = new Vector3(_startingPoint.x, _startingPoint.y + (i * _startingPointYOffset), 0);
            if (nextParticipantId == _myParticipantId)
            {
                // 4
                if (i == 1)
                {
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
                    playerCopy = (Instantiate(PlayerPrefab, carStartPoint, Quaternion.Euler(0,0,180)) as GameObject);
                }
                else
                {
                    playerCopy = (Instantiate(PlayerPrefab, carStartPoint, Quaternion.identity) as GameObject);
                }
                playerCopy.transform.SetParent(Foreground);
                playerCopy.GetComponent<PlayerController>().SetPlayerNumber(i + 1);
                
            }
            else
            {
                // 5
                GameObject opponentPlayer;

                if (i==1)
                {
                    opponentPlayer = (Instantiate(opponentPrefab, carStartPoint, Quaternion.Euler(0, 0, 180)) as GameObject);
                }
                else
                {
                    opponentPlayer = (Instantiate(opponentPrefab, carStartPoint, Quaternion.identity) as GameObject);
                }
                
                opponentPlayer.transform.SetParent(Foreground);
                OpponentController opponentScript = opponentPlayer.GetComponent<OpponentController>();
                opponentScript.SetPlayerNumber(i + 1);
                // 6
                _opponentScripts[nextParticipantId] = opponentScript;

                if (i == 1)
                {
                    Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
        // 7
        _multiplayerReady = true;
    }

    void Update()
    {
        if (_multiplayerReady)
        {
            DoMultiplayerUpdate();
        }

        if (_showLobbyDialog)
        {
            debugMessagesLobbby.text = _lobbyMessage;
        }
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

    void DoMultiplayerUpdate()
    {
        if (Time.time > _nextBroadcastTime)
        {
            MultiplayerController.Instance.SendMyUpdate(playerCopy.transform.position.x,
                                            playerCopy.transform.position.y,
                                            playerCopy.transform.rotation.eulerAngles.z);
            _nextBroadcastTime = Time.time + .16f;
        }
    }

    public void UpdateReceived(string senderId, float posX, float posY, float rotZ)
    {
        debugText.text = "Update received";

        if (_multiplayerReady)
        {
            OpponentController opponent = _opponentScripts[senderId];
            if (opponent != null)
            {
                opponent.SetCarInformation(posX, posY, rotZ);
            }
        }
    }
}
