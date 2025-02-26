using UnityEngine;

public class MeshExplosiveEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private float explosionForce = 100f;    // The strength of the explosion
    [SerializeField]private float explosionRadius = 1f;      // The radius of the explosion effect
    [SerializeField]private float explosionStartTime = 0.05f;   // The time before the explosion happens
    [SerializeField] private float fragmentsLifeTime = 3f;

    [SerializeField] private LayerMask layerMask; // The layermask to filter what objects the explosion affects
    
    private Vector3 explosionPosition;       // Where the explosion happens

    void Start()
    {
        Invoke("ApplyExplosionForce", explosionStartTime);
        Destroy(gameObject, fragmentsLifeTime + explosionStartTime);
        
    }

    void ApplyExplosionForce()
    {
        explosionPosition = transform.position;
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius, layerMask);

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
