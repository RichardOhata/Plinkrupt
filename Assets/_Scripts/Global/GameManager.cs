using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //player default preset if there is no saving data.
    [SerializeField] private PlayerDefaultPreset _playerDefaultPreset;
    [SerializeField] public ElementMultiplierTable elementMultiplierTable;

    // global variables
    public float currentMoney = 0;
    public bool outOfBalls = false;

    //TODO: May refactor to a different class
    [HideInInspector] public BonusScoring bonusScoring = new BonusScoring();

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

    void Update()
    {
        if(outOfBalls && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            outOfBalls = false;
            TransitionManager.instance.OpenEndOfRoundMenu();
        }
    }

    void LoadDefaultPreset(){
        this.currentMoney = _playerDefaultPreset.playerStartingMoney;
    }

    /// <summary>
    /// Updates the current money by applying a multiplier to the given bid and returns the calculated amount.
    /// </summary>
    /// <param name="multipliter">The multiplier to apply to the bid.</param>
    /// <returns>The amount of money added to the current total after applying the multiplier to the bid.</returns>

    public float UpdateMoneyWithMultiplier(float multipliter)
    {
        float money = 100 * multipliter;
        this.currentMoney += money;
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
        return money;
    }
}
