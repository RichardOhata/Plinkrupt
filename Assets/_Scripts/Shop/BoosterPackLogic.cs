using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static ShopLogic;

public class BoosterPackLogic : MonoBehaviour
{
    private GameObject shopWindow;
    private bool isSelected;
    private ShopLogic shopLogic;

    public Collider objectCollider;
    public Animator animator;

    public BoosterPackItem[] items;
    public GameObject itemCardPrefab;

    private float selectionTime = -1f;
    private float selectionCooldown = 1.0f;
    private void Start()
    {
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
        shopLogic = shopWindow.GetComponent<ShopLogic>();
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {
            if ((Time.time - selectionTime) >= selectionCooldown &&
                !IsPointerOverUIObject() && !IsPointerOverGameObject())
            {
                DeselectCard();
            }
        }
    }

    // Functions for detecting inputs
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

    // Handles for when the booster pack is selected
    public void HandleSelect()
    {
        if (shopLogic.currentSelectedCard != null && shopLogic.currentSelectedCard != gameObject)
        {
            shopLogic.currentSelectedCard.GetComponent<BoosterPackLogic>().DeselectCard(); // Unselects any other boosterpack is that currently selected
        }
        if (!isSelected)
        {
            shopLogic.currentSelectedCard = gameObject;
            PlayCardAnimation(true);
            isSelected = true;
            selectionTime = Time.time;
            shopLogic.DisplayUseButton();
        
        }
    }

    private void PlayCardAnimation(bool isUp)
    {
        string triggerName = GetComponent<ConsumableConfig>().cardpos switch
        {
            CardPos.MiddlePos => isUp ? "CardMiddleUp" : "CardMiddleDown",
            CardPos.RightPos => isUp ? "CardRightUp" : "CardRightDown",
            CardPos.LeftPos => isUp ? "CardLeftUp" : "CardLeftDown",
            _ => "CardMiddleUp" // Default failsafe
        };

        animator.SetTrigger(triggerName);
    }

    public void DeselectCard()
    {
        if (!isSelected) return;

        PlayCardAnimation(false);
        shopLogic.HideUseButton();
        isSelected = false;
        shopLogic.currentSelectedCard = null;
    }
}
