using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    Inventory inventory;
	// Use this for initialization
	void Start () {
        inventory = GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "trap")
        {
            Destroy(target.gameObject);
            inventory.Kill();  
        } else if (target.gameObject.tag == "money")
        {
            Destroy(target.gameObject);
            inventory.CollectMoney(10); //TODO get it from other object
        }
    }

}
