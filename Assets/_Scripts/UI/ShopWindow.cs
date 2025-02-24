using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class ShopWindow : MonoBehaviour
{
    public GameObject useDropPanel;
    public Transform cardHorizontalLayout;
    public GameObject elementalCardPrefab;
    public ElementPack[] elements;
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
        card.GetComponent<ElementCardUI>().SetData(elements[UnityEngine.Random.Range(0, elements.Length)]);
        }
    }

    public void HandleElementCardUse(ElementPack element)
    {
        element.IncrementMult();
        foreach(Transform child in cardHorizontalLayout)
        {
            Destroy(child.gameObject);
        }
    }
}
