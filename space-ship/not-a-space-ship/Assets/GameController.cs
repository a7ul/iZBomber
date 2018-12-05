using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text moneyView;
    public Text gdprCountView;
    public Text contactLessView;
    public Text ecommView;
    public GameObject lobbyCamera;
    public GameObject lobbyCanvas;
    public GameObject InGameCanvas;


    // Use this for initialization
    void Start () {

    }

    public void SetMoney(float money)
    {
        moneyView.text = "Money: " + money;
    }

    public void SetGDPRCount(float gdpr)
    {
        gdprCountView.text = "GDPR: " + gdpr;
    }

    public void SetContactLessCount(float money)
    {
        contactLessView.text = "CL: " + money;
    }

    public void SetEcommCount(float count)
    {
        ecommView.text = "Ecomm: " + count;
    }

    public void SetLobbyActive(bool active)
    {
        lobbyCamera.SetActive(active);
        lobbyCanvas.SetActive(active);
        InGameCanvas.SetActive(!active);
    }

    [PunRPC]
    public void PublishGlobalMessage(string msg)
    {
        Debug.Log(msg);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
