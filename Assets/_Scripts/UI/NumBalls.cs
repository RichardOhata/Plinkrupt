using UnityEngine;
using TMPro;

public class NumBalls : MonoBehaviour
{
    private BallSpawner ballSpawner;
    private TMP_Text total;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballSpawner = GameObject.Find("BallSpawner").GetComponent<BallSpawner>();
        total = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        total.text = ballSpawner.numBalls.ToString();
    }
}
