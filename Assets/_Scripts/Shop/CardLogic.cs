using TMPro;
using UnityEngine;
using static ShopLogic;
using UnityEngine.EventSystems;

public class CardLogic : MonoBehaviour
{
    private GameObject shopWindow;
    public Animator animator;
    public Collider objectCollider;
    private bool isSelected;

    public BoosterPackItem item;

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
            CardPos cardPos = GetComponent<ConsumableConfig>().cardpos;
            // Determine the correct trigger based on position
            string triggerName = cardPos switch
            {
                CardPos.MiddlePos => "CardMiddleUp",
                CardPos.RightPos => "CardRightUp",
                CardPos.LeftPos => "CardLeftUp",
                _ => "CardMiddleUp" // Default case (failsafe)
            };

            // Set the animation trigger
            animator.SetTrigger(triggerName);
            isSelected = true;
            shopWindow.GetComponent<ShopLogic>().DisplayUseButton();
            shopWindow.GetComponent<ShopLogic>().currentSelectedCard = gameObject;
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
