using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreDisplay : MonoBehaviour
{

    public TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        if(GameManager.Instance == null){
            Debug.LogWarning("Game Manager instance is not set !");
            return;
        }
        if(scoreText == null){
            scoreText = GetComponent<TMP_Text>();
        }
        GameManager.Instance.OnScoreUpdatedEvent += UpdateScore;
    }
    void Start(){
        UpdateScore(GameManager.Instance.currentMoney);
    }


    private void UpdateScore(float money)
    {
        scoreText.text = $"${money}";
    }
}
