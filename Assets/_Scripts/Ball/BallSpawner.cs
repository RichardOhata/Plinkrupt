using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    int numBalls = 10;

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
        }

        dropButton.interactable = true;

        GameObject.Find("GameManager").GetComponent<GameManager>().outOfBalls = true;
    }

    public void SetNumBalls(int newNumBalls)
    {
        numBalls = newNumBalls;
    }
}
