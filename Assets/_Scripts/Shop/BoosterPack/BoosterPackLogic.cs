using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class BoosterPackLogic : MonoBehaviour
{
    private GameObject shopWindow;
    private bool isSelected;
    public Collider objectCollider;
    private void Start()
    {
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {

            if (!IsPointerOverUIObject() && !IsPointerOverGameObject())
            {
                shopWindow.GetComponent<ShopLogic>().HideUseButton();
                isSelected = false;
                shopWindow.GetComponent<ShopLogic>().currentSelectedCard = null;
            }
        }
    }

    public void HandleUse()
    {
        isSelected = true;
        shopWindow.GetComponent<ShopLogic>().DisplayUseButton();
        shopWindow.GetComponent<ShopLogic>().currentSelectedCard = gameObject;
    }

    private bool IsPointerOverGameObject()
    {
        Vector3 worldPoint = GetWorldPointFromScreen(Input.mousePosition);
        return objectCollider.bounds.Contains(worldPoint);
    }

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private Vector3 GetWorldPointFromScreen(Vector3 screenPosition)
    {
        screenPosition.z = 0f; // Adjust this if your collider is at a different depth
        return screenPosition;
    }

 
}
