using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text moneyView;
    public Text gdprCountView;
    public Text contactLessView;
    public Text ecommView;


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

    // Update is called once per frame
    void Update () {
		
	}
}
