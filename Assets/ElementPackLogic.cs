using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ElementPackLogic : MonoBehaviour
{
    public ElementPack[] elements;
    public GameObject elementCardUI;
    public InputAction position, press;
    private PlayerControls playerController; // Testing tap inputs

    private void Awake()
    {
        playerController = new PlayerControls();
        press = playerController.FindAction("Press");
        position = playerController.FindAction("Position");
        position.Enable();
        press.Enable();
        press.performed += _ => { Debug.Log("Tap"); UpdateText(); };
        press.canceled += _ => Debug.Log("Tap Done");
    }

    public void UpdateText()
    {
        elementCardUI.GetComponent<ElementCardUI>().SetData(elements[Random.Range(0, elements.Length)].elementType);
    }
}
