using UnityEngine;

public class InitialSpin : MonoBehaviour
{
    private Rigidbody rb;
    public float spinForce = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Generate a random direction for the spin
            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized * spinForce;

            // Apply torque to add initial spin
            rb.AddTorque(randomTorque, ForceMode.Impulse);    }}

    // Update is called once per frame
    void Update()
    {
        
    }
}
