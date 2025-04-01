using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    public GameObject useDropPanel;
    public Transform cardHorizontalLayout;
    public Transform shopContents;
    public GameObject elementalCardPrefab;
    public GameObject boosterPackPrefab;
    public ElementPack[] elements;
    public int rerollPrice = 3;

    private readonly float[] xPositions = { -630f, 0f, 630.54f }; // for demo purposes
    private const float yPosition = 543f; // Remove later
    public void ShowUsePanel()
    {
        useDropPanel.SetActive(true);
    }

    public void HideUsePanel()
    {
        useDropPanel.SetActive(false);
    }

    public void ShowElementalCards()
    {
        for (int index = 0; index < 3; index++) { 
        GameObject card = Instantiate(elementalCardPrefab, cardHorizontalLayout);
        card.GetComponent<ElementCardUI>().SetData(elements[UnityEngine.Random.Range(0, elements.Length)], index);
        }
    }

    public void ResetPosition(Transform elementalCard, int index)
    {
        if (elementalCard.transform.parent != cardHorizontalLayout)
        {
            elementalCard.transform.SetParent(cardHorizontalLayout);
        }
            elementalCard.SetSiblingIndex(index);
    }

    public void HandleElementCardUse(ElementPack element)
    {
        element.IncrementMult();
        foreach(Transform child in cardHorizontalLayout)
        {
            Destroy(child.gameObject);
        }
    }


    public void Reroll()
    {
        foreach (Transform child in shopContents)
        {
            Destroy(child.gameObject);
        }

        foreach (float x in xPositions)
        {
            GameObject boosterPack = Instantiate(boosterPackPrefab, shopContents);
            RectTransform rectTransform = boosterPack.GetComponent<RectTransform>(); // remove later for demo purposes
            rectTransform.anchoredPosition = new Vector2(x, yPosition);
        }

        ScoreManager.Instance.UpdateMoney(-rerollPrice);
    }
    }
