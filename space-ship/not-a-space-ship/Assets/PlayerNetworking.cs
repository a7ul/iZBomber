using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : MonoBehaviour
{

    [SerializeField]
    private GameObject playerCamera;

    [SerializeField]
    private MonoBehaviour[] scriptsToIgnore;


    PhotonView photonView;
    public string PlayerName;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    void Update()
    {
        PhotonNetwork.player.NickName = PlayerName;
        GetComponentInChildren<TextMesh>().text = PlayerName;
    }

    void Initialize()
    {
        PlayerName = "Player" + photonView.owner.ID;

        if (photonView.isMine)
        {

        }
        else
        {
            playerCamera.SetActive(false);
            foreach (MonoBehaviour behaviour in scriptsToIgnore)
            {
                behaviour.enabled = false;
            }
        }
    }
}
