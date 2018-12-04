using System.Collections;
using UnityEngine;

public class Trap : Photon.MonoBehaviour {

    private readonly WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    readonly float trapActivationTime = 1.5f;
    private LineRenderer laserLine;
    Rigidbody2D rb;
    float bombDistance = 5f;
    PhotonView photonView;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        laserLine = GetComponent<LineRenderer>();
        photonView = GetComponent<PhotonView>();

        StartCoroutine(ActivateTrap());
    }

    void FixedUpdate()
    {
       
    }

    private IEnumerator ActivateTrap()
    {

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / trapActivationTime;
            yield return null;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, bombDistance);
        laserLine.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            StartCoroutine(BoomEffect());
            laserLine.SetPosition(1, hit.point);
            string colliderTag = hit.collider.gameObject.tag;
            if (colliderTag  == "Player")
            {
               Inventory inventory =  hit.collider.gameObject.GetComponent<Inventory>();
               inventory.Kill();
            }
            else if(colliderTag == "money")
            {
                PhotonNetwork.Destroy(hit.collider.gameObject);

            }
        }
        else
        {
            StartCoroutine(BoomEffect());
            laserLine.SetPosition(1, new Vector2(transform.position.x, transform.position.y - bombDistance));

        }
    }

    private IEnumerator BoomEffect()
    {
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
        if (photonView.isMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
