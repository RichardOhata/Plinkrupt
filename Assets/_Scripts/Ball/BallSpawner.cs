using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject[] ballPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SpawnBall()
    {
        //place a bid
        GameManager.Instance.PlaceBid(GameManager.Instance.currentBid);

        //spawn a ball
        GameObject ball = Instantiate(ballPrefab[Random.Range(0, ballPrefab.Length)], transform.position, Quaternion.identity);

        //set the current bid value to the ball
        IElementScoreInteraction elementScoreInteraction = ball.GetComponent<IElementScoreInteraction>();
        elementScoreInteraction.setCurrentBid(GameManager.Instance.currentBid);

    }
}
