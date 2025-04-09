using TMPro;
using UnityEngine;
using static ShopLogic;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine.UIElements;

public class CardLogic : MonoBehaviour
{
    private GameObject shopWindow;
    public Animator animator;
    public Collider objectCollider;
    private bool isSelected;

    public BoosterPackItem item;

    public Action<Boolean> OnCardSelectedEvent;
    private float selectionTime = -1f;
    private float selectionCooldown = 1.0f;
    private void Start()
    {
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
        objectCollider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {
            if ((Time.time - selectionTime) >= selectionCooldown &&  // Wait before allowing deselect
            !IsPointerOverUIObject() &&
            !IsPointerOverGameObject())
            {
                //OnCardSelectedEvent?.Invoke(false); // Notify that the card is deselected
                DeselectCard();

            }
        }
    }



    public void SetData(BoosterPackItem item)
    {
        this.item = item;

    }

    public void HandleUse()
    {
        if (shopWindow.GetComponent<ShopLogic>().currentSelectedCard != null && shopWindow.GetComponent<ShopLogic>().currentSelectedCard != gameObject)
        {
            shopWindow.GetComponent<ShopLogic>().currentSelectedCard.GetComponent<CardLogic>().DeselectCard();
        }
        if (!isSelected)
        {
            shopWindow.GetComponent<ShopLogic>().currentSelectedCard = gameObject;
            CardPos cardPos = GetComponent<ConsumableConfig>().cardpos;
            // Determine the correct trigger based on position
            string triggerName = cardPos switch
            {
                CardPos.MiddlePos => "CardMiddleUp",
                CardPos.RightPos => "CardRightUp",
                CardPos.LeftPos => "CardLeftUp",
                _ => "CardMiddleUp" // Default case (failsafe)
            };

            //OnCardSelectedEvent?.Invoke(true); // Notify that the card is selected

            // Set the animation trigger
            animator.SetTrigger(triggerName);
       
            animator.SetBool("isCardRotating", true);
        
            isSelected = true;
            selectionTime = Time.time;
           
            shopWindow.GetComponent<ShopLogic>().DisplayUseButton();
        }
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
    private void ReverseAnimation()
    {
        CardPos cardPos = GetComponent<ConsumableConfig>().cardpos;

        // Determine the correct trigger for the "down" animation
        string triggerName = cardPos switch
        {
            CardPos.MiddlePos => "CardMiddleDown",
            CardPos.RightPos => "CardRightDown",
            CardPos.LeftPos => "CardLeftDown",
            _ => "CardMiddleDown" // Default failsafe
        };

        animator.SetTrigger(triggerName);
        animator.SetBool("isCardRotating", false);
    }

    public void DeselectCard()
    {
        if (isSelected)
        {
            ReverseAnimation();
            shopWindow.GetComponent<ShopLogic>().HideUseButton();
            isSelected = false;
            shopWindow.GetComponent<ShopLogic>().currentSelectedCard = null; // Reset selected card
        }
    }
}
