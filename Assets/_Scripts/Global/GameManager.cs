using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        if(Instance == null){
            Instance = this;
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_playerDefaultPreset != null){
            LoadDefaultPreset();
        }
    }

    void LoadDefaultPreset(){
        this.currentMoney = _playerDefaultPreset.playerStartingMoney;
    }

    /// <summary>
    /// Places a bid of the given amount and subtracts that amount from the current money total.
    /// </summary>
    /// <param name="bid">The amount to bid.</param>
    public void PlaceBid(float bid){
        this.currentMoney -= bid;
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
    }

    /// <summary>
    /// Updates the current money by applying a multiplier to the given bid and returns the calculated amount.
    /// </summary>
    /// <param name="bid">The bid amount to be multiplied.</param>
    /// <param name="multipliter">The multiplier to apply to the bid.</param>
    /// <returns>The amount of money added to the current total after applying the multiplier to the bid.</returns>

    public float UpdateMoneyWithMultiplier(float bid, float multipliter)
    {
        float money = bid * multipliter;
        this.currentMoney += money;
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
        return money;
    }
}
