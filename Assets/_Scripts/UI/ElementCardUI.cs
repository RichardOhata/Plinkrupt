using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCardUI : MonoBehaviour
{
    private GameObject shopWindow;
    private Animator animator;
    public TextMeshProUGUI elementText;
    public ElementPack element;
    private int index;
    private void Start()
    {
        DragDropManager.AddObject(GetComponent<ObjectSettings>());
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
        animator = shopWindow.GetComponent<Animator>();
    }

    public void SetData(ElementPack element, int index)
    {
        this.index = index;
        this.element = element;
        elementText.text = element.elementType.ToString();
        GetComponent<Image>().color = element.color;
    }

    public void ShowUsePanel()
    {
        shopWindow.GetComponent<ShopWindow>().ShowUsePanel();
    }

    public void HideUsePanel()
    {
            
        shopWindow.GetComponent<ShopWindow>().HideUsePanel();
        shopWindow.GetComponent<ShopWindow>().ResetPosition(transform, index);

    }

    public void HandleUse()
    {
        animator.SetTrigger("WindowUp");
        shopWindow.GetComponent<ShopWindow>().HandleElementCardUse(element);
        DragDropManager.RemoveObject(GetComponent<ObjectSettings>());
        Destroy(gameObject);
    }
}
