using UnityEngine;

public class MeshExplosiveEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float explosionForce = 100f;    // The strength of the explosion
    public float explosionRadius = 1f;      // The radius of the explosion effect
    public Vector3 explosionPosition;       // Where the explosion happens

    public bool isExplode = false;

    void Start()
    {
        Invoke("ApplyExplosionForce", 1f);

    }

    void ApplyExplosionForce()
    {
        explosionPosition = transform.position;
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            // Check if the object has a Rigidbody
            if (rb != null)
            {
                // Apply explosion force to the Rigidbody
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
