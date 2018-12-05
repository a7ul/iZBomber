using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CustomLineRenderer
{
    public LineRenderer Line { get; set; }
    public Vector2 Direction { get; set; }
}

public class Trap : Photon.MonoBehaviour
{

    private readonly WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    readonly float trapActivationTime = 1.5f;
    Rigidbody2D rb;
    float bombDistance = 5f;
    PhotonView photonView;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        List<RaycastHit2D> hits = new List<RaycastHit2D>(new RaycastHit2D[] {
            Physics2D.Raycast(transform.position, Vector2.up, bombDistance),
            Physics2D.Raycast(transform.position, Vector2.right, bombDistance),
            Physics2D.Raycast(transform.position, Vector2.down, bombDistance),
            Physics2D.Raycast(transform.position, Vector2.left, bombDistance),
        });

        List<CustomLineRenderer> laserLines = new List<CustomLineRenderer>
        {
            new CustomLineRenderer { 
                Line = transform.Find("lr1").GetComponent<LineRenderer>(),
                Direction = new Vector2(transform.position.x, transform.position.y + bombDistance)
            },
            new CustomLineRenderer {
                Line = transform.Find("lr2").GetComponent<LineRenderer>(),
                Direction = new Vector2(transform.position.x + bombDistance, transform.position.y)
            },
            new CustomLineRenderer {
                Line = transform.Find("lr3").GetComponent<LineRenderer>(),
                Direction = new Vector2(transform.position.x, transform.position.y - bombDistance)
            },
            new CustomLineRenderer {
                Line = transform.Find("lr4").GetComponent<LineRenderer>(),
                Direction = new Vector2(transform.position.x  - bombDistance, transform.position.y)
            }
        };


        for (int i = 0; i < hits.Count; i++)
        {
            RaycastHit2D hit = hits[i];
            CustomLineRenderer laserLine = laserLines[i];
            laserLine.Line.SetPosition(0, transform.position);

            StartCoroutine(BoomEffect(laserLine.Line));

            if (hit.collider != null)
            {
                laserLine.Line.SetPosition(1, hit.point);
                string colliderTag = hit.collider.gameObject.tag;
                if (colliderTag == "Player")
                {
                    Inventory inventory = hit.collider.gameObject.GetComponent<Inventory>();
                    inventory.Kill();
                }
                else if (colliderTag == "money")
                {
                    PhotonNetwork.Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                laserLine.Line.SetPosition(1, laserLine.Direction);
            }
        }
    }

    private IEnumerator BoomEffect(LineRenderer laserLine)
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
