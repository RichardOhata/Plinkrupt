using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeDetection : MonoBehaviour
{
    private PlayerControls playerController;
    public static SwipeDetection instance;
    public delegate void Swipe(Vector2 direction);
    public event Swipe swipePerformed;
    public InputAction position, press;

    [SerializeField] private float swipeResistance = 100;

    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        playerController = new PlayerControls();
        press = playerController.FindAction("Press");
        position = playerController.FindAction("Position");
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();
        instance = this;
    }

    private void DetectSwipe()
    {
        Debug.Log("Here");
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        // Check horizontal swipe
        if (Mathf.Abs(delta.x) > swipeResistance)
        {
            direction.x = Mathf.Sign(delta.x); // Use sign to map to -1 or 1
        }

        // Check vertical swipe
        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Sign(delta.y); // Use sign to map to -1 or 1
        }

        // Fire event if a valid swipe direction is detected
        if (direction != Vector2.zero && swipePerformed != null)
        {
            Debug.Log("Swipe happened");
            swipePerformed(direction);
         
        }
    }
}
