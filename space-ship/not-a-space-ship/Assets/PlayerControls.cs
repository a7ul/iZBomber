using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : Photon.MonoBehaviour
{

    Rigidbody2D rb;
    Inventory inventory;
    Animator anim;

    Vector2 old_pos;

    bool facingRight = false;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        anim = GetComponent<Animator>();
        old_pos = transform.position;
    }


    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(horizontalMovement , verticalMovement , 0);

        // Actions
        if (Input.GetKeyDown(KeyCode.Space) && inventory.money >= 20)
        {
            inventory.CollectMoney(-20);
            PhotonNetwork.Instantiate("Trap", transform.position, Quaternion.identity, 0);
        }
    }

    void Flip()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.flipX = facingRight;
        facingRight = !facingRight;
    }

    void CheckAndFlip()
    {
        float axis =  transform.position.x - old_pos.x;
        if (axis > 0 && !facingRight)
        { Flip(); }
        else if (axis < 0 && facingRight)
        { Flip(); }

    }

    void AnimatePlayerOnMovement()
    {


        if (old_pos.Equals(transform.position))
        {
            anim.SetTrigger("Stay"); 
        }
        else
        {
            CheckAndFlip();
            anim.SetTrigger("Move");
        }
        anim.Update(0);
        old_pos = transform.position;
    }

    void FixedUpdate()
    {
        MovePlayer();
        AnimatePlayerOnMovement();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(facingRight);
        }
        else
        {
            facingRight = (bool)stream.ReceiveNext();
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.flipX = !facingRight;
         }
    }
}
