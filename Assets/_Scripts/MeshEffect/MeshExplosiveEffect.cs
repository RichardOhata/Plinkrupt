using System;
using UnityEngine;

public class MeshExplosiveEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private float _explosionForce = 100f;    // The strength of the explosion
    [SerializeField]private float _explosionRadius = 1f;      // The radius of the explosion effect
    [SerializeField]private float _explosionStartTime = 0.05f;   // The time before the explosion happens
    [SerializeField]private float _fragmentsLifeTime = 10f;

    [SerializeField] private LayerMask layerMask; // The layermask to filter what objects the explosion affects
    
    private Vector3 explosionPosition;       // Where the explosion happens

    public float FragmentsLifeTime { get => _fragmentsLifeTime; set => _fragmentsLifeTime = value; }

    void Start()
    {
        Invoke("ApplyExplosionForce", _explosionStartTime);
        Destroy(gameObject, _fragmentsLifeTime + _explosionStartTime);
    }

    void ApplyExplosionForce()
    {
        explosionPosition = transform.position;
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius, layerMask);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            // Check if the object has a Rigidbody
            if (rb != null)
            {
                // Apply explosion force to the Rigidbody
                rb.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}
