using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShopLogic : MonoBehaviour
{
    private Vector3 leftPos = new Vector3(-3f, 0.2f, -0.2f);
    private Vector3 middlePos = new Vector3(0f, 0.2f, -0.2f);
    private Vector3 rightPos = new Vector3(3f, 0.2f, -0.2f);
    
    public BoosterPack[] boosterPacks;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button useButton;

    [SerializeField]
    private Button rerollButton;
    private int rerollCost = 300;

    [SerializeField]
    private Button nextRoundButton;


    [SerializeField]
    private GameObject gameBoard;

    public enum ConsumableType{
        Boosterpack,
        Card,
    }

    public enum CardPos
    {
        LeftPos,
        MiddlePos,
        RightPos,
    }


    public GameObject currentSelectedCard;

    //void Start()
    //{
    //    InstantiateBoosterPacks();
    //}

    private void OnEnable()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        InstantiateBoosterPacks();
        rerollCost = 300;
        rerollButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reroll $" + rerollCost;
    }

    private void InstantiateBoosterPacks()
    {
        CreateBoosterPack(leftPos, CardPos.LeftPos);
        CreateBoosterPack(middlePos, CardPos.MiddlePos);
        CreateBoosterPack(rightPos, CardPos.RightPos);
    }

    private void CreateBoosterPack(Vector3 position, CardPos cardPos)
    {
        BoosterPack selectedPack = boosterPacks[UnityEngine.Random.Range(0, boosterPacks.Length)];
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        GameObject boosterPack = Instantiate(selectedPack.boosterPackPrefab, position, rotation);

        boosterPack.transform.SetParent(content.transform, false);
        boosterPack.transform.localPosition = position;

        boosterPack.GetComponent<ConsumableConfig>().cardpos = cardPos;

        BoosterPackLogic pack = boosterPack.GetComponent<BoosterPackLogic>();
        if (pack != null)
        {
            pack.items = selectedPack.itemPool;
            pack.itemCardPrefab = selectedPack.cardPrefab;
        }
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
            case ConsumableType.Boosterpack:
                ScoreManager.Instance.UpdateMoney(-300);
                ReplaceContent(currentSelectedCard.GetComponent<BoosterPackLogic>().items, currentSelectedCard.GetComponent<BoosterPackLogic>().itemCardPrefab);
                rerollButton.interactable = false;
                nextRoundButton.interactable = false;
                break;
            case ConsumableType.Card:
                HandleCardUse(currentSelectedCard.GetComponent<CardLogic>().item);
                rerollButton.interactable = true;
                nextRoundButton.interactable = true;
                break;
            default: 
                break;
        }
        Destroy(currentSelectedCard);
        currentSelectedCard = null;
    }

    public void ReplaceContent(BoosterPackItem[] items, GameObject itemCardPrefab)
    {
        HideUseButton();
        rerollButton.interactable = false;
        // Remove all children under 'content'
        foreach (Transform child in content.transform)
        {
            child.gameObject.SetActive(false);
        }
       
        // Spawn three elemental cards at the same positions
        CreateCard(leftPos, CardPos.LeftPos, itemCardPrefab, items);
        CreateCard(middlePos, CardPos.MiddlePos, itemCardPrefab, items);
        CreateCard(rightPos, CardPos.RightPos, itemCardPrefab, items);

    }

    private void CreateCard(Vector3 position, CardPos cardPos, GameObject cardPrefab, BoosterPackItem[] items)
    {
        BoosterPackItem selectedItem = items[UnityEngine.Random.Range(0, items.Length)];

        GameObject card = Instantiate(cardPrefab, position, Quaternion.identity);
        CardLogic cardUI = card.GetComponent<CardLogic>();   
           
               cardUI.SetData(selectedItem);

        card.transform.SetParent(content.transform, false);
        card.transform.localPosition = position;
        card.GetComponent<ConsumableConfig>().cardpos = cardPos;
    }

    public void HandleCardUse(BoosterPackItem card)
    {
        card.PerformAction();

        foreach (Transform child in content.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
        HideUseButton();
    }

    public void Reroll()
    {
        if (ScoreManager.Instance.currentMoney < 300)
        {
            Debug.Log("Not enough money to reroll!");
            return;  
        }

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        currentSelectedCard = null;
        HideUseButton();
        InstantiateBoosterPacks();

        ScoreManager.Instance.UpdateMoney(-300);
        rerollCost = rerollCost * 2;
        rerollButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reroll $" + rerollCost;
    }
}
