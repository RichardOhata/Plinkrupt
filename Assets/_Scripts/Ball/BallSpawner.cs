using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    int numBalls = 1;

    public GameObject[] ballPrefab;
    private Button dropButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        dropButton = GameObject.Find("Drop").GetComponent<Button>();
    }

    public void SpawnBall()
    {
        dropButton.interactable = false;

        //place the bid
        GameManager.Instance.PlaceBid(GameManager.Instance.currentBid * numBalls);

        StartCoroutine(BallSpawn());
    }

    IEnumerator BallSpawn()
    {
        for (int i = 0; i < numBalls; i++)
        {
            //instantiate ball object
            GameObject ball = Instantiate(ballPrefab[Random.Range(0, ballPrefab.Length)], transform.position, Quaternion.identity);

            //set the current bid value to the ball
            IElementScoreInteraction elementScoreInteraction = ball.GetComponent<IElementScoreInteraction>();
            elementScoreInteraction.setCurrentBid(GameManager.Instance.currentBid);

            yield return new WaitForSeconds(0.4f);
        }

        dropButton.interactable = true;
    }

    public void SetNumBalls(int newNumBalls)
    {
        numBalls = newNumBalls;
    }
}
