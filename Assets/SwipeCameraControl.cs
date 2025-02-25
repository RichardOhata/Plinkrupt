using UnityEngine;

public class SwipeOrbitCamera : MonoBehaviour
{
    public Transform target;  // The central object to orbit around
    public float sensitivity = 0.1f; // Adjust rotation speed
    public float distance = 5f; // Manually set zoom factor

    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    private float rotationY = 0f; // Only horizontal rotation

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("No target assigned for orbit camera.");
            return;
        }

        UpdateCameraPosition();
    }

    void Update()
    {
        HandleTouchInput();
        UpdateCameraPosition();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 delta = touch.deltaPosition;
                        rotationY += delta.x * sensitivity; // Rotate only horizontally
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }

    void UpdateCameraPosition()
    {
        if (target == null) return;

        // Compute only horizontal rotation
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Update camera position and look at the target
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
