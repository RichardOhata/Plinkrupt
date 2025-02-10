using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject[] ballPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SpawnBall()
    {
        GameManager.instance.PlaceBid(GameManager.instance.currentBid);
        GameObject ball = Instantiate(ballPrefab[Random.Range(0, ballPrefab.Length)], transform.position, Quaternion.identity);
        IElementScoreInteraction elementScoreInteraction = ball.GetComponent<IElementScoreInteraction>();
        elementScoreInteraction.setCurrentBid(GameManager.instance.currentBid);

    }
}
