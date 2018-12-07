using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject player;

    public GameController gameController;

    private Button joinBtn;
    private InputField nameInput;


    void Start () {
        PhotonNetwork.ConnectUsingSettings("1.0");
        joinBtn = gameController.lobbyCanvas.GetComponentInChildren<Button>();
        nameInput = gameController.lobbyCanvas.GetComponentInChildren<InputField>();

        joinBtn.onClick.AddListener(HandleOnJoinClick);
    }

    void OnGUI()
    {
        // GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    void HandleOnJoinClick()
    {
        if (PhotonNetwork.connected && PhotonNetwork.inRoom)
        {
            if (nameInput.text == "SPECTATE")
            {
                gameController.lobbyCanvas.SetActive(false);
                return;
            }

            PhotonNetwork.player.NickName = nameInput.text;

            gameController.SetLobbyActive(false);
            PhotonNetwork.Instantiate("Player", player.transform.position, Quaternion.identity, 0);
        }
    }

    void OnJoinedLobby () {
        PhotonNetwork.JoinOrCreateRoom(
            "Room",
            new RoomOptions(){ MaxPlayers = 15 },
            TypedLobby.Default
        );
    }

    void OnJoinedRoom () {

    }
}
