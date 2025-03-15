using System.Collections.Generic;
using Esper.ESave;
using NUnit.Framework;
using UnityEngine;
[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(SaveFileSetup))]
public class ScoreManager : MonoBehaviour
{   
    // global variables
    public static ScoreManager Instance { get; private set; }

    [Header("Element Multiplier Table")]
    [SerializeField] public ElementMultiplierTable elementMultiplierTable;
    public float currentMoney = 0;

    // private variables

    //player default preset if there is no saving data.
    [Header("Player Preset Setting")]
    [SerializeField] private PlayerDefaultPreset _playerDefaultPreset;

    private ScoringWindow _ScoringWindow; //cached Scoring Window
    private SaveFile _saveFile; // save

    // Element Records tracking
    private List<ElementScoreRecord> _storedScoringRecord;
    public SerializableDictionary<ElementClass.ElementType, float> _elementTotals = new SerializableDictionary<ElementClass.ElementType, float>();
    private ElementScoreRecord _currentLeadingElement;

    //Events
    public event System.Action<float> OnScoreUpdatedEvent;
    public event System.Action<(Color, float)> OnLeadingElementChangedEvent;

    //encapsulated Properties
    public ScoringWindow ScoringWindow { get => _ScoringWindow; set => _ScoringWindow = value; }
    public List<ElementScoreRecord> StoredScoringRecord { get => _storedScoringRecord; set => _storedScoringRecord = value; }

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
        _saveFile = GetComponent<SaveFileSetup>().GetSaveFile();
        _storedScoringRecord = new List<ElementScoreRecord>();
        _elementTotals = new SerializableDictionary<ElementClass.ElementType, float>();
        _currentLeadingElement = new ElementScoreRecord(ElementClass.ElementType.Air, 0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_playerDefaultPreset != null){
            LoadMoneyData();
        }
        //cache the scoring window
        if(_ScoringWindow == null){
            _ScoringWindow = FindFirstObjectByType<ScoringWindow>();
        }
        Assert.IsNotNull(_ScoringWindow, "No Scoring Window Found");
    }

    void LoadMoneyData(){
        if(_saveFile.HasData("Money")){
            this.currentMoney = _saveFile.GetData<float>("Money");
        }
        else{
            this.currentMoney = _playerDefaultPreset.playerStartingMoney;
        }
        OnScoreUpdatedEvent?.Invoke(this.currentMoney);
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

    //keep track of the scoring record
    public void UpdateElementMultiplierRecord(ElementClass.ElementType ElementType, float totalScore){
        _storedScoringRecord.Add(new ElementScoreRecord(ElementType, totalScore));

        //add to element totals
        if(_elementTotals.ContainsKey(ElementType)) _elementTotals[ElementType] += totalScore;
        else _elementTotals.Add(ElementType, totalScore);

        foreach (var element in _elementTotals){
            //update the current leading element
            if(element.Value > _currentLeadingElement.score){

                //calculate the percentage change of the leading element
                float scoreChangeFactor = (element.Value - _currentLeadingElement.score) / _currentLeadingElement.score;
                
                //multiply the percentage change by the total score
                scoreChangeFactor = totalScore * scoreChangeFactor;

                OnLeadingElementChangedEvent?.Invoke((elementMultiplierTable.GetElementColor(element.Key), scoreChangeFactor));
                _currentLeadingElement = new ElementScoreRecord(element.Key, element.Value);
            }
        }
        

        Debug.Log($"Element: {ElementType}, Score: {totalScore}, Color: {elementMultiplierTable.GetElementColor(ElementType)}");
        //update the scoring window
        ScoringWindow.AddScore(elementMultiplierTable.GetElementColor(ElementType), totalScore);
    }
}
