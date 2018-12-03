using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworking : MonoBehaviour
{

    [SerializeField]
    private GameObject playerCamera;

    [SerializeField]
    private MonoBehaviour[] scriptsToIgnore;

    public string PlayerName;
    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    void Update()
    {
       
    }

    void Initialize()
    {
        GetComponentInChildren<TextMesh>().text = photonView.owner.NickName;

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
