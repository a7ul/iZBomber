using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).

    readonly float trapActivationTime = 3f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

        // Cast a ray straight up.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        Debug.Log(hit.collider);
        // If it hits something...
        if (hit.collider != null)
        {
            Debug.Log("BOOM");

            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            float force = liftForce * heightError - rb.velocity.y * damping;

            rb.AddForce(Vector3.up * force);
        }
    }
}
