using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public int numBalls = 10;

    public float currentBidValue = 100;
    public GameObject ballPrefab;
    public BallConfigSO[] ballConfigSOs;
    //cached Game Managers
    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _gameManager = GameManager.Instance;

    }

    public void SpawnBall()
    {
        StartCoroutine(BallSpawn());
    }

    IEnumerator BallSpawn()
    {
        for (int i = 0; i < numBalls; i++)
        {
            //instantiate ball object
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);

            BaseBallController ballController = new BaseBallController.Builder()
                .WithConfig(ballConfigSOs[Random.Range(0, ballConfigSOs.Length)])
                .WithCurrentBid(currentBidValue)
                .Build(ball);

            _gameManager.currentBalls--;
            yield return new WaitForSeconds(0.4f);
        }

    }

    public void SetNumBalls(int newNumBalls)
    {
        numBalls = newNumBalls;
    }
}
