using UnityEngine;
using TMPro;

public class NumBalls : MonoBehaviour
{
    private TMP_Text total;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        total = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        total.text = GameManager.Instance.currentBalls.ToString();
    }
}
