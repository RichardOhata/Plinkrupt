using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCardUI : MonoBehaviour
{
    public TextMeshProUGUI elementText;
    public GameObject useButton;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    public ElementPack element;

    private void Awake()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    private void OnEnable()
    {
            InputManager.instance.OnTap += HandleTap;
    }

    private void OnDisable()
    {
            InputManager.instance.OnTap -= HandleTap;
    }

    public void SetData(ElementPack element)
    {
        this.element = element;
        elementText.text = element.elementType.ToString();

    }

    private void HandleTap(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
            {
                useButton.SetActive(!useButton.activeSelf);
            }
        }
    }
}
