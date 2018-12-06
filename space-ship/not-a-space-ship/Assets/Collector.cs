using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    Inventory inventory;
    LootGenerator lg;

    // Use this for initialization
    void Start () {
        inventory = GetComponent<Inventory>();
        lg = FindObjectOfType<LootGenerator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D target)
    {
        switch (target.gameObject.tag)
        {
            case "trap":
                Destroy(target.gameObject);
                inventory.Kill();
                break;
            case "money":
                Destroy(target.gameObject);
                lg.currentSpawns--;
                inventory.CollectMoney(10); //TODO get it from other object
                break;
            case "powerup":
                Destroy(target.gameObject);
                lg.currentSpawns--;
                inventory.ActivatePaypal();
                break;
        }
    }

}
