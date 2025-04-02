using UnityEngine;

public class TransitionTriggerZone : MonoBehaviour
{

    public GameObject objectToActivate; // Assign this in the Inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your character has the "Player" tag
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // Activate the target object
            }
        }
    }
}
