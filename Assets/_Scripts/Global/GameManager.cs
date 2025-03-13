using Esper.ESave;
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

    //save
    private SaveFile _saveFile;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            
        }else{
            Debug.LogWarning("Game Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
        
        _saveFile = GetComponent<SaveFileSetup>().GetSaveFile();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_playerDefaultPreset != null){
            LoadMoneyData();
        }
    }

    void Update()
    {
        if(outOfBalls && GameObject.FindGameObjectsWithTag("ball").Length == 0)
        {
            SceneManager.LoadScene("DiegoEndOfRound");
        }
    }

    void LoadMoneyData(){
        if(_saveFile.HasData("Money")){
            this.currentMoney = _saveFile.GetData<float>("Money");
        }
        else{
            this.currentMoney = _playerDefaultPreset.playerStartingMoney;
        }
    }
    
    public void SaveMoneyData(){
        _saveFile.AddOrUpdateData("Money", this.currentMoney);
        _saveFile.Save();
    }

    public void UpdateMoney(float amount){
        this.currentMoney += amount;
        SaveMoneyData();
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
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
        SaveMoneyData();

        return money;
    }

}
