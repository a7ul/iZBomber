using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Photon.MonoBehaviour {
    public float money;
    public float noOfContactless;
    public float gdprCount;
    public float ecommCount;

    private GameController gc;

    private void SetValues()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            gc.SetMoney(money);
            gc.SetGDPRCount(gdprCount);
            gc.SetEcommCount(ecommCount);
            gc.SetContactLessCount(ecommCount);
        }
    }

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        SetValues();
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        money -= damage;
        SetValues();
    }

    [PunRPC]
    public void CollectMoney(float amount)
    {
        money += amount;
        SetValues();
    }



}
