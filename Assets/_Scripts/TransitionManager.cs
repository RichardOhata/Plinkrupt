using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public GameObject board;
    public GameObject shopMenu;
    public GameObject endOfRoundMenu;
    public GameObject gameUIMenu;
    public GameObject mainGameCamera;

    [SerializeField]
    private int round;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
        round = 0;
    }

    // Call when all balls have been used
    public void OpenEndOfRoundMenu()
    {
        //board.SetActive(false);
        //shopMenu.SetActive(true);
        endOfRoundMenu.SetActive(true);
    }

    public void OpenShop()
    {
        mainGameCamera.SetActive(false);
        //board.SetActive(false);
        gameUIMenu.SetActive(false);
        shopMenu.SetActive(true);


        endOfRoundMenu.SetActive(false);
    }
    public void CloseShop()
    {
        board.SetActive(true);
        shopMenu.SetActive(false);
        gameUIMenu.SetActive(true);
        endOfRoundMenu.SetActive(false);
        mainGameCamera.SetActive(true);
        GameManager.Instance.openEndofRoundFlag = false;
        GameManager.Instance.ResetBalls();
        ScoreManager.Instance.ResetSideScore();
        GameManager.Instance.ResetBoard();
    }

    public void IncreaseRound()
    {
        round++;
    }

    public int GetRound() {
        return round;
    }
}
