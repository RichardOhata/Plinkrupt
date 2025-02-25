using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public int blockType; // Type of block (1-6)
    public int collisionThreshold = 4; // Number of hits before destruction
    private int collisionCount = 0; // Tracks collisions with the ball

    public float explosionForce = 7f; // Strength of explosion
    public float explosionRadius = 0.5f; // Radius of effect
    public float upwardModifier = 1f; // Upward push effect

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
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
                }
            }
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
