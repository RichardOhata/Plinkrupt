using System;
using UnityEngine;


public class BoosterPackLogic : MonoBehaviour
{
    private GameObject shopWindow;
    private Animator animator;
    private void Start()
    {
        DragDropManager.AddObject(GetComponent<ObjectSettings>());
        shopWindow = GameObject.FindGameObjectWithTag("ShopWindow");
        animator = shopWindow.GetComponent<Animator>();
    }


    public void HandleUse()
    {
        animator.SetTrigger("WindowDown");
        ScoreManager.Instance.currentMoney -= 300;
        shopWindow.GetComponent<ShopWindow>().ShowElementalCards();
        DragDropManager.RemoveObject(GetComponent<ObjectSettings>());
        Destroy(gameObject);
    }

    public void ShowUsePanel()
    {
        shopWindow.GetComponent<ShopWindow>().ShowUsePanel();
    }

    public void HideUsePanel()
    {   
        shopWindow.GetComponent<ShopWindow>().HideUsePanel();
    }

   
}
