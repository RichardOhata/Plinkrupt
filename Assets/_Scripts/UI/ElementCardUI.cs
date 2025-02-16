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
    public ElementPack element;
    public Animator animator;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("ShopWindow").GetComponent<Animator>();
    }
    private void OnEnable()
    {
            //InputManager.instance.OnTap += HandleTap;
    }

    private void OnDisable()
    {
            //InputManager.instance.OnTap -= HandleTap;
    }

    public void SetData(ElementPack element)
    {
        this.element = element;
        elementText.text = element.elementType.ToString();

    }

    public void HandleTap()
    {
                useButton.SetActive(!useButton.activeSelf);
    }

    public void HandleUseButton()
    {
        element.IncrementMult();
        animator.SetTrigger("WindowUp");
        Destroy(gameObject);
    }
}
