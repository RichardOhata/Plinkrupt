using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public int numBalls = 10;

    public GameObject ballPrefab;
    public BallConfigSO[] ballConfigSOs;
    //cached Game Managers
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    
    //ball queue control
    [Header("Ball Queue Control")]
    public Queue<GameObject> _ballQueue = new Queue<GameObject>();
    public float ballQueueTime = 0.4f;
    [SerializeField]private float _ballQueueTimer = 0f;
    [SerializeField]private bool isBallDropping = false;
    private Button _dropButton;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //initialise the GameManager and ScoreManager
        _gameManager = GameManager.Instance;
        _scoreManager = ScoreManager.Instance;


        _gameManager.currentBalls = numBalls;
        _gameManager.OnBallDroppingEvent += SpawnBall;
    }

    public void initialBalls(float currentBidValue){
        //initialise the ball queue
        for (int i = 0; i < numBalls; i++)
        {
            //instantiate ball object
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            BaseBallController ballController = new BaseBallController.Builder()
                .WithConfig(ballConfigSOs[Random.Range(0, ballConfigSOs.Length)])
                .WithCurrentBid(currentBidValue)
                .Build(ball);

            ball.SetActive(false);
            _ballQueue.Enqueue(ball);
            
        }
    }

    public void Update()
    {
        if(_ballQueueTimer > ballQueueTime){
            if(isBallDropping && _ballQueue.Count > 0){
                //check if the ball spawner is set
                GameObject ball = _ballQueue.Dequeue();

                // Update game state 
                _gameManager.currentBalls--;
                _scoreManager.UpdateMoney(-_gameManager.currentBidValue);
                
                ball.SetActive(true);
                //reset timer
                _ballQueueTimer = 0f;
            }
        }
        _ballQueueTimer += Time.deltaTime;
    }


    //spawn ball toggle
    public void SpawnBall(bool isDropping){
        isBallDropping = isDropping;
    }

    // public void SpawnBall()
    // {
    //     StartCoroutine(BallSpawn());
    // }

    // IEnumerator BallSpawn()
    // {
    //     for (int i = 0; i < numBalls; i++)
    //     {
    //         //instantiate ball object
    //         GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

    //         BaseBallController ballController = new BaseBallController.Builder()
    //             .WithConfig(ballConfigSOs[Random.Range(0, ballConfigSOs.Length)])
    //             .WithCurrentBid(currentBidValue)
    //             .Build(ball);

    //         _gameManager.currentBalls--;
    //         yield return new WaitForSeconds(0.4f);
    //     }
    // }

    public void SetNumBalls(int newNumBalls)
    {
        numBalls = newNumBalls;
    }
    private void OnDestroy()
    {
        _gameManager.OnBallDroppingEvent -= SpawnBall;        
    }
}
