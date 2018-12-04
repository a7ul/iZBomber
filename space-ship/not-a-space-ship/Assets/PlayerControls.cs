using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : Photon.MonoBehaviour {

    Rigidbody2D rb;
    public float speed;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
        // Movement
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        // Actions
         if (Input.GetKeyDown(KeyCode.Space))
        {
            PhotonNetwork.Instantiate("Trap", transform.position, Quaternion.identity, 0); 
        }
    }
}
