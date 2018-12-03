using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject lobbyCamera;

    [SerializeField]
    private GameObject lobbyCanvas;

    private Button joinBtn;
    private InputField nameInput;


    void Start () {
        PhotonNetwork.ConnectUsingSettings("1.0");
        joinBtn = lobbyCanvas.GetComponentInChildren<Button>();
        nameInput = lobbyCanvas.GetComponentInChildren<InputField>();

        joinBtn.onClick.AddListener(HandleOnJoinClick);
    }

    void OnGUI()
    {
        // GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    void HandleOnJoinClick()
    {
        PhotonNetwork.player.NickName = nameInput.text;
        lobbyCamera.SetActive(false);
        lobbyCanvas.SetActive(false);
        PhotonNetwork.Instantiate("Player", player.transform.position, Quaternion.identity, 0); 
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
