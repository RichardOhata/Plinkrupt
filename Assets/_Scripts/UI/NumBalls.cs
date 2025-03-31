using UnityEngine;
using TMPro;

public class NumBalls : MonoBehaviour
{
    private BallSpawner ballSpawner;
    private TMP_Text total;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballSpawner = GameManager.Instance.ballSpawner;
        if (ballSpawner == null)
        {
            Debug.LogWarning("Ball Spawner is not set!");
            return;
        }
        total = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        total.text = GameManager.Instance.currentBalls.ToString();
    }
}
