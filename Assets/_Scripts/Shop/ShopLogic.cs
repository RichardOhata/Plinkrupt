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

    [SerializeField]
    private GameObject boosterPackPrefab;
    [SerializeField]
    private GameObject elementalCardPrefab;
    [SerializeField]
    private GameObject oracleCardPrefab;
    
    public BoosterPack[] boosterPacks;

    public ElementPack[] elements;

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

    [SerializeField]
    private ScoringAreaConfigSO[] scoringAreaConfigSOs;
    public enum ConsumableType{
        ElementalBoosterPack,
        ElementalCard,
        OracleBoosterPack,
        OracleCard
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

    private void OnEnable()
    {
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
        // Pick a random booster pack from the array
        GameObject randomBoosterPackPrefab = boosterPacks[UnityEngine.Random.Range(0, boosterPacks.Length)].boosterPackPrefab;

        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        GameObject boosterPack = Instantiate(randomBoosterPackPrefab, position, rotation);

        // Set the booster pack as a child of 'content'
        boosterPack.transform.SetParent(content.transform, false);

        // Keep its local position the same as the given world position
        boosterPack.transform.localPosition = position;

        // Assign the enum value to the booster pack
        boosterPack.GetComponent<ConsumableConfig>().cardpos = cardPos;
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
        ScoreManager.Instance.UpdateMoney(-300);
        ConsumableType type = currentSelectedCard.GetComponent<ConsumableConfig>().GetConsumableType();
        switch (type)
        {
            case ConsumableType.ElementalBoosterPack:
                ReplaceContent(true);
                rerollButton.interactable = false;
                nextRoundButton.interactable = false;
                break;
            case ConsumableType.ElementalCard:
                HandleElementCardUse(currentSelectedCard.GetComponent<ElementCardUI>().element);
                rerollButton.interactable = true;
                nextRoundButton.interactable = true;
                break;
            case ConsumableType.OracleBoosterPack:
                ReplaceContent(false);
                rerollButton.interactable = false;
                nextRoundButton.interactable = false;
                break;
            case ConsumableType.OracleCard:
                HandleOracleCardUse(currentSelectedCard.GetComponent<OracleCardUI>().scoringAreaElement);
                rerollButton.interactable = true;
                nextRoundButton.interactable = true;
                break;
            default: 
                break;
        }
        Destroy(currentSelectedCard);
        currentSelectedCard = null;
    }

    public void ReplaceContent(bool isElement)
    {
        HideUseButton();
        rerollButton.interactable = false;
        // Remove all children under 'content'
        foreach (Transform child in content.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (isElement)
        {
            // Spawn three elemental cards at the same positions
            CreateElementalCard(leftPos, CardPos.LeftPos);
            CreateElementalCard(middlePos, CardPos.MiddlePos);
            CreateElementalCard(rightPos, CardPos.RightPos);
        }else
        {
            CreateOracleCard(leftPos, CardPos.LeftPos);
            CreateOracleCard(middlePos, CardPos.MiddlePos);
            CreateOracleCard(rightPos, CardPos.RightPos);
        }
     
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

    private void CreateOracleCard(Vector3 position, CardPos cardPos)
    {
        var scoringAreas = gameBoard.GetComponent<HexMountainGenerator>().scoringAreaGameObject;
        GameObject oracleCard = Instantiate(oracleCardPrefab, position, Quaternion.identity);
        oracleCard.GetComponent<OracleCardUI>().SetData(scoringAreaConfigSOs[UnityEngine.Random.Range(0, scoringAreaConfigSOs.Length)].getScoringAreaConfig());
        // Set it as a child of 'content'
        oracleCard.transform.SetParent(content.transform, false);

        // Keep its local position the same as the given world position
        oracleCard.transform.localPosition = position;
        oracleCard.GetComponent<ConsumableConfig>().cardpos = cardPos;
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

    public void HandleOracleCardUse(ElementMultiplierConfig scoringAreaConfig)
    {
        var scoringAreas = gameBoard.GetComponent<HexMountainGenerator>().scoringAreaGameObject;
        GameObject randomScoringArea = scoringAreas[UnityEngine.Random.Range(0, scoringAreas.Count)];
        while (randomScoringArea.GetComponent<ScoringTriggerZone>().getElementType() == scoringAreaConfig.elementType)
        {
           randomScoringArea = scoringAreas[UnityEngine.Random.Range(0, scoringAreas.Count)];
        }
        randomScoringArea.GetComponent<ScoringTriggerZone>().UpdateSetting(scoringAreaConfig);
        
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
