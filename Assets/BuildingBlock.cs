using UnityEngine;
using System.Collections.Generic;

public class BuildingBlock : MonoBehaviour
{
    public int blockType; // Type of block (1-6)
    public int collisionThreshold = 4; // Number of hits before destruction
    private int collisionCount = 0; // Tracks collisions with the ball

    public float explosionForce = 7f; // Strength of explosion
    public float explosionRadius = 0.5f; // Radius of effect
    public float upwardModifier = 1f; // Upward push effect
    public float contactTimeThreshold = 3f; // Time before self-destruct if ball stays

    private Dictionary<GameObject, float> touchingBalls = new Dictionary<GameObject, float>();

    void Update()
    {
        CheckLongContact();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            collisionCount++;

            if (collisionCount >= collisionThreshold)
            {
                Explode();
                SelfDestruct();
            }

            // Start tracking contact time
            if (!touchingBalls.ContainsKey(collision.gameObject))
            {
                touchingBalls[collision.gameObject] = Time.time;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            touchingBalls.Remove(collision.gameObject);
        }
    }

    void CheckLongContact()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var kvp in touchingBalls)
        {
            if (Time.time - kvp.Value >= contactTimeThreshold)
            {
                Explode();
                SelfDestruct();
                return; // Prevent multiple explosions
            }
        }
    }

    void Explode()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider obj in nearbyObjects)
        {
            if (obj.CompareTag("ball"))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Generate a random sideways force
                    Vector3 randomSideForce = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

                    // Explosion force direction: Mix explosion with sideways force
                    Vector3 explosionDirection = (obj.transform.position - transform.position).normalized + randomSideForce * 0.5f;
                    explosionDirection.y += upwardModifier; // Keep upward push

                    // Apply explosion force
                    rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                }
            }
        }
    }


    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
