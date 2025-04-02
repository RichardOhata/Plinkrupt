using TMPro;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public GameObject board;
    public GameObject shopMenu;
    public GameObject endOfRoundMenu;
    public GameObject gameUIMenu;
    public GameObject mainGameCamera;
    public AudioSource playMusic;
    public AudioSource shopMusic;

    [SerializeField]
    private int round;

    public bool isBossPhase = false;
    private float bossRequiredScore = 500f;

    [SerializeField]
    private GameObject bossUI;
    [SerializeField]
    private GameObject gameOverScreen;
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

    public void Start()
    {
        playMusic.Play();
    }

    // Call when all balls have been used
    public void OpenEndOfRoundMenu()
    {
        endOfRoundMenu.SetActive(true);
        if (isBossPhase)
        {
            CheckBossCondition();
        }
        playMusic.Stop();
        shopMusic.Play();
    }

    public void OpenShop()
    {
        mainGameCamera.SetActive(false);
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
        round++;
        if (round % 3 == 0) // Every fourth round
        {
            StartBossPhase();
        }
        shopMusic.Stop();
        playMusic.Play();
    }

    public int GetRound() {
        return round;
    }

    private void StartBossPhase()
    {
        isBossPhase = true;
        bossUI.SetActive(true);
        bossUI.GetComponentInChildren<TextMeshProUGUI>().text = "Boss Requirement: Must achieve a score of " + bossRequiredScore + " to proceed";
    }

    // Call to check if boss conditon has been met
    private void CheckBossCondition()
    {
        float accScore = 0;
        foreach (var record in ScoreManager.Instance.StoredScoringRecord)
        {
            accScore += record.score;
            Debug.Log($"Element: {record.elementType}, Score: {record.score}");
        }
        if (accScore >= bossRequiredScore)
        {
            Debug.Log(accScore);
            isBossPhase = false;
            bossUI.SetActive(false);
            bossRequiredScore *= 1.2f;
        } else
        {
            // Lose
            gameOverScreen.SetActive(true);
        }
       
    }
}
