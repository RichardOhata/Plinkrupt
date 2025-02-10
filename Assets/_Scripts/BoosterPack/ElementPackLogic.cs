using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ElementPackLogic : MonoBehaviour
{
    public ElementPack[] elements;
    public GameObject elementCardUI;

    private void Awake()
    {
        //InputManager.instance.Press.performed += _ => UpdateText();
    }

    public void UpdateText()
    {
        //elementCardUI.GetComponent<ElementCardUI>().SetData(elements[Random.Range(0, elements.Length)].elementType.ToString());
    }
}
