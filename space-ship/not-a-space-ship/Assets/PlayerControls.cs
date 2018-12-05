using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : Photon.MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    Inventory inventory;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void FixedUpdate() {
        // Movement
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        // Actions
        if (Input.GetKeyDown(KeyCode.Space) && inventory.money >= 10)
        {
            inventory.CollectMoney(-10);
            PhotonNetwork.Instantiate("Trap", transform.position, Quaternion.identity, 0); 
        }
    }
}
