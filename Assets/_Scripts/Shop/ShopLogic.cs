using System;
using UnityEngine;
using UnityEngine.UI;


public class ShopLogic : MonoBehaviour
{
    private Vector3 leftPos = new Vector3(-3f, 0.2f, -0.2f);
    private Vector3 middlePos = new Vector3(0f, 0.2f, -0.2f);
    private Vector3 rightPos = new Vector3(3f, 0.2f, -0.2f);

    [SerializeField]
    private GameObject boosterPackPrefab;
    [SerializeField]
    private GameObject elementalCardPrefab;

    public ElementPack[] elements;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button useButton;

    public enum ConsumableType{
        ElementalBoosterPack,
        ElementalCard
    }

    public enum CardPos
    {
        LeftPos,
        MiddlePos,
        RightPos,
    }


    public GameObject currentSelectedCard;

    void Start()
    {
        InstantiateBoosterPacks();
    }

    private void InstantiateBoosterPacks()
    {
        CreateBoosterPack(leftPos, CardPos.LeftPos);
        CreateBoosterPack(middlePos, CardPos.MiddlePos);
        CreateBoosterPack(rightPos, CardPos.RightPos);
    }

    private void CreateBoosterPack(Vector3 position, CardPos cardPos)
    {
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        GameObject boosterPack = Instantiate(boosterPackPrefab, position, rotation);

        // Set the booster pack as a child of 'content'
        boosterPack.transform.SetParent(content.transform, false);

        // Keep its local position the same as the given world position
        boosterPack.transform.localPosition = position;

        // Assign the enum value to the booster pack
        boosterPack.GetComponent<ConsumableConfig>().cardpos = cardPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  

    public void DisplayUseButton()
    {
        useButton.gameObject.SetActive(true);
    }

    public void HideUseButton()
    {
        useButton.gameObject.SetActive(false);
    }

    public void HandleUseButton()
    {
        ConsumableType type = currentSelectedCard.GetComponent<ConsumableConfig>().GetConsumableType();
        switch (type)
        {
            case ConsumableType.ElementalBoosterPack:
                ReplaceContent();
                break;
            case ConsumableType.ElementalCard:
                HandleElementCardUse(currentSelectedCard.GetComponent<ElementCardUI>().element);
                break;
            default: 
                break;
        }
        Destroy(currentSelectedCard);
        currentSelectedCard = null;
    }

    public void ReplaceContent()
    {
        HideUseButton();
        // Remove all children under 'content'
        foreach (Transform child in content.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Spawn three elemental cards at the same positions
        CreateElementalCard(leftPos, CardPos.LeftPos);
        CreateElementalCard(middlePos, CardPos.MiddlePos);
        CreateElementalCard(rightPos, CardPos.RightPos);

     
    }

    private void CreateElementalCard(Vector3 position, CardPos cardPos)
    {
        GameObject elementalCard = Instantiate(elementalCardPrefab, position, Quaternion.identity);
        elementalCard.GetComponent<ElementCardUI>().SetData(elements[UnityEngine.Random.Range(0, elements.Length)], 0);
        // Set it as a child of 'content'
        elementalCard.transform.SetParent(content.transform, false);

        // Keep its local position the same as the given world position
        elementalCard.transform.localPosition = position;
        elementalCard.GetComponent<ConsumableConfig>().cardpos = cardPos;
    }

    public void HandleElementCardUse(ElementPack element)
    {
        element.IncrementMult();
        foreach (Transform child in content.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            } else
            {
                child.gameObject.SetActive(true);
            }
         
        }
        HideUseButton();
    }

    public void Reroll()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        currentSelectedCard = null;
        HideUseButton();
        InstantiateBoosterPacks();

        GameManager.Instance.currentMoney -= 3;
    }
}
