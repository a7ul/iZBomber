using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Cast a ray straight up.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.DrawRay(transform.position, Vector2.up, Color.green);
        Debug.Log(hit.collider);
        // If it hits something...
        if (hit.collider != null)
        {

            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            float force = liftForce * heightError - rb.velocity.y * damping;

            // Apply the force to the rigidbody.
            rb.AddForce(Vector3.up * force);
        }
    }
}
