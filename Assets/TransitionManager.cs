using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject board;
    public GameObject shopMenu;
    public GameObject endOfRoundMenu;
    public GameObject gameUIMenu;

    // Call when all balls have been used
    public void OpenEndOfRoundMenu()
    {
        board.SetActive(false);
        shopMenu.SetActive(false);
        endOfRoundMenu.SetActive(true);
    }


    public void OpenShop()
    {
        board.SetActive(false);
        shopMenu.SetActive(true);
        endOfRoundMenu.SetActive(false);
    }

    public void CloseShop()
    {
        board.SetActive(true);
        shopMenu.SetActive(false);
        endOfRoundMenu.SetActive(false);
    }
}
