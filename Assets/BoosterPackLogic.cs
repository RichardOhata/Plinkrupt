using System;
using UnityEngine;

public class BoosterPackLogic : MonoBehaviour
{
    public GameObject useButton;
    public Animator animator;
    public Transform parentCanvas;
    public GameObject elementalCard;

    public ElementPack[] elements;
    public void HandleTap()
    {
        useButton.SetActive(!useButton.activeSelf);
    }

    public void HandleUseButton()
    {
        animator.SetTrigger("WindowDown");
        ShowElementalCards();
        //gameObject.SetActive(false);
    }

    private void ShowElementalCards()
    {
        GameObject card = Instantiate(elementalCard, parentCanvas);
        card.GetComponent<ElementCardUI>().SetData(elements[UnityEngine.Random.Range(0, elements.Length)]);
    }
}
