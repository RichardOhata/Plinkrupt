using UnityEngine;
using TMPro;

public class RoundNumber : MonoBehaviour
{

    private TMP_Text round;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        round = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        round.text = (TransitionManager.instance.GetRound() + 1).ToString();
    }
}
