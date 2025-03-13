using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public int numBalls = 10;

    public GameObject[] ballPrefab;
    private Button dropButton;

    //cached Game Managers
    private GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        dropButton = GameObject.Find("Drop").GetComponent<Button>();
        _gameManager = GameManager.Instance;
    }

    public void SpawnBall()
    {
        dropButton.interactable = false;

        StartCoroutine(BallSpawn());
    }

    IEnumerator BallSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            //instantiate ball object
            GameObject ball = Instantiate(ballPrefab[Random.Range(0, ballPrefab.Length)], transform.position, Quaternion.identity);

            //set the current bid value to the ball
            IElementScoreInteraction elementScoreInteraction = ball.GetComponent<IElementScoreInteraction>();
            elementScoreInteraction.setCurrentBid(100);

            yield return new WaitForSeconds(0.4f);

            numBalls--;
        }

        dropButton.interactable = true;

        //out of balls

        _gameManager.outOfBalls = true;
    }

    public void SetNumBalls(int newNumBalls)
    {
        numBalls = newNumBalls;
    }
}
