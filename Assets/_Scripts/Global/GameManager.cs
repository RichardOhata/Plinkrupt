using Esper.ESave;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ball dropping control")]
    public BallSpawner ballSpawner;
    public Button dropButton;
    public int currentBalls = 0;
    public int currentBallInstances = 0;

    public bool openEndofRoundFlag = false;
    //TODO: May refactor to a different class
    [HideInInspector] public BonusScoring bonusScoring = new BonusScoring();

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
    }

    public void SpawnBall()
    {
        
        //check if the ball spawner is set
        if(ballSpawner == null){
            Debug.LogWarning("Ball Spawner is not set!");
            return;
        }
        ballSpawner.SpawnBall();
    }


    


}
