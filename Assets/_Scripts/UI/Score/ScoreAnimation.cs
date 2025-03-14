using UnityEngine;
using DG.Tweening;
using TMPro;


public class ScoreAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private int _startValue = 0;
    [SerializeField] private int _endValue = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
