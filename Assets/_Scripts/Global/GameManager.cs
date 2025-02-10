using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //player default preset if there is no saving data.
    [SerializeField] private PlayerDefaultPreset _playerDefaultPreset;
    [SerializeField] public ElementMultiplierTable elementMultiplierTable;

    // global variables
    public float currentMoney = 0;

    //TODO: current bid should/may refector to a different class
    public float currentBid = 100;


    //Events
    public event System.Action<float> OnScoreUpdatedEvent;

    void OnEnable(){
        if(instance == null){
            instance = this;
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_playerDefaultPreset != null){
            loadDefaultPreset();
        }
    }

    void loadDefaultPreset(){
        this.currentMoney = _playerDefaultPreset.playerStartingMoney;
    }

    public void PlaceBid(float bid){
        this.currentMoney -= bid;
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
    }

    public float UpdateMoneyWithMultiplier(float bid, float multipliter)
    {
        float money = bid * multipliter;
        this.currentMoney += money;
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
        return money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
