using System;
using UnityEngine;


public class BoosterPackLogic : MonoBehaviour
{
    private GameObject shopWindow;
    private Animator animator;
    public Transform parentCanvas;
    public GameObject elementalCard;
    public ElementPack[] elements;
    private void Start()
    {
        DragDropManager.AddObject(GetComponent<ObjectSettings>());
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
        animator = shopWindow.GetComponent<Animator>();
    }


    public void HandleUse()
    {
        animator.SetTrigger("WindowDown");
        ShowElementalCards();
        gameObject.SetActive(false);
    }

    public void ShowUsePanel()
    {
        shopWindow.GetComponent<ShopWindow>().ShowUsePanel();
    }

    public void HideUsePanel()
    {   
        shopWindow.GetComponent<ShopWindow>().HideUsePanel();
    }

    private void ShowElementalCards()
    {
        DragDropManager.RemoveObject(GetComponent<ObjectSettings>());
        GameObject card = Instantiate(elementalCard, parentCanvas);
        card.GetComponent<ElementCardUI>().SetData(elements[UnityEngine.Random.Range(0, elements.Length)]);
        Destroy(gameObject);
    }
}
