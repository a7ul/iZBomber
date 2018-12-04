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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "trap")
        {
            Destroy(other.gameObject);
            inventory.TakeDamage(5); //TODO get it from other object
            return;
        }
        if (other.gameObject.tag == "money")
        {
            Destroy(other.gameObject);
            inventory.CollectMoney(10); //TODO get it from other object
        }
    }

}
