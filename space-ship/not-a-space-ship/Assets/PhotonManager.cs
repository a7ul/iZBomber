using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject lobbyCamera;


    void Start () {
        PhotonNetwork.ConnectUsingSettings("1.0");
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby () {
        PhotonNetwork.JoinOrCreateRoom(
            "Room",
            new RoomOptions(){ MaxPlayers = 5 },
            TypedLobby.Default
        );
    }

    void OnJoinedRoom () {
        PhotonNetwork.Instantiate("Player", player.transform.position, Quaternion.identity, 0);
        lobbyCamera.SetActive(false); 
    }
}
