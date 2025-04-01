using System;
using Esper.ESave;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ball dropping control")]
    public BallSpawner ballSpawner;
    public HexMountainGenerator boardSpawner;
    public Button dropButton;
    public int currentBalls = 0;
    public int currentBallInstances = 0;

    [Header("Bid value control")]
    public float currentBidValue = 100;

    public bool openEndofRoundFlag = false;
    //TODO: May refactor to a different class
    [HideInInspector] public BonusScoring bonusScoring = new BonusScoring();

    public event Action<bool> OnBallDroppingEvent;
    public UnityEvent OnBallDroppingUnityEvent;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetBalls();
    }

    void Update()
    {
        //all the ball instances are destroyed, open the end of round menu
        if(currentBallInstances <= 0 && !openEndofRoundFlag){
            TransitionManager.instance.OpenEndOfRoundMenu();
            openEndofRoundFlag = true;
        }
    }

    public void ResetBalls()
    {
        dropButton.interactable = true;
        currentBalls = ballSpawner.numBalls;
        currentBallInstances = ballSpawner.numBalls;
        ballSpawner.initialBalls(currentBidValue);
    }
    public void ResetBoard() {
        boardSpawner.resetBoard();
    }

    public void SpawnBall()
    {
        OnBallDroppingEvent?.Invoke(true);
        OnBallDroppingUnityEvent?.Invoke();
    }

    public void EndBallDropping()
    {
        OnBallDroppingEvent?.Invoke(false);
        OnBallDroppingUnityEvent?.Invoke();
    }
}
