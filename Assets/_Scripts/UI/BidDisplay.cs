using TMPro;
using UnityEngine;

public class BidDisplay : MonoBehaviour
{
    public TMP_Text bidText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        if(GameManager.Instance == null){
            Debug.LogWarning("Game Manager instance is not set !");
            return;
        }
        if(bidText == null){
            bidText = GetComponent<TMP_Text>();
        }
    }
    void Update(){
        bidText.text = "$" + GameManager.Instance.currentBid.ToString();
    }



}
