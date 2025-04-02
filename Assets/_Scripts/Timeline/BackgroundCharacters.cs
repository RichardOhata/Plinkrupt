using UnityEngine;

public class BackgroundCharacters : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", speed);
        lastPosition = transform.position;
    }
}
