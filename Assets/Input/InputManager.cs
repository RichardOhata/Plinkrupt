using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Singleton Instance of Player Controls
public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    private PlayerControls playerControls;

    private InputAction press, position;

    public event Action<Vector2> OnTap;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // This ensures that there will only be one instance of the input manager
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        playerControls = new PlayerControls();
        playerControls.Enable();

        press = playerControls.FindAction("Press");
        position = playerControls.FindAction("Position");

        press.performed += ctx => DetectTap();
    }

    private void DetectTap()
    {
        Vector2 screenPos = position.ReadValue<Vector2>();
        OnTap?.Invoke(screenPos);
    }
}
