using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class ScoringTriggerZone : MonoBehaviour, IElementScoreInteraction
{
    //cached game manager references
    private ScoreManager _currentScoreManager;

    //configs
    [SerializeField]private ScoringAreaConfigSO _scoringAreaConfigSo;
    [SerializeField]private ElementClass.ElementType _objectElement;

    [SerializeField]private Color _elementColor;
    [SerializeField]private float _baseMultiplier;
    private bool _isLoaded = false;


    public event Action<Color> OnScoreAreaVisualChangeEvent;
    public ScoringAreaConfigSO[] scoringAreaConfigSOs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("here");
            ElementMultiplierConfig scoringAreaConfig = scoringAreaConfigSOs[UnityEngine.Random.Range(0, scoringAreaConfigSOs.Length)].getScoringAreaConfig();
            UpdateSetting(scoringAreaConfig);

        }
    }
    void OnEnable(){
        _currentScoreManager = ScoreManager.Instance;
    }
    void Start()
    {
        if(!_scoringAreaConfigSo){
            Debug.LogWarning("Scoring Trigger So is not set!");
            return;
        }
        if(!ScoreManager.Instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        if(_scoringAreaConfigSo != null){
            _isLoaded = true;
            LoadSetting(_scoringAreaConfigSo.getScoringAreaConfig());   
        }
    }

    public void LoadSetting(ElementMultiplierConfig scoringAreaConfig){

        if(_isLoaded) return;
        //set configs
        _isLoaded = true;
        _objectElement = scoringAreaConfig.elementType;
        _baseMultiplier = scoringAreaConfig.multiplier;
        _elementColor = scoringAreaConfig.elementColor;

        OnScoreAreaVisualChangeEvent?.Invoke(_elementColor);
        ElementMultiplierManager.Instance.MultiplierAddedEvent += AddElementMultiplier;
        ElementMultiplierManager.Instance.MultiplierRemovedEvent += RemoveElementMultiplier;
    }

    public void UpdateSetting(ElementMultiplierConfig scoringAreaConfig)
    {
        //set configs
        _isLoaded = true;
        _objectElement = scoringAreaConfig.elementType;
        _baseMultiplier = scoringAreaConfig.multiplier;
        _elementColor = scoringAreaConfig.elementColor;

        OnScoreAreaVisualChangeEvent?.Invoke(_elementColor);
        ElementMultiplierManager.Instance.MultiplierAddedEvent += AddElementMultiplier;
        ElementMultiplierManager.Instance.MultiplierRemovedEvent += RemoveElementMultiplier;
    }
    /// <summary>
    /// Handles the trigger enter event. When a collision occurs, it checks if the colliding object 
    /// implements the IElementScoreInteraction interface. If so, calculates the score using the 
    /// element multiplier and updates the game manager with the new score.
    /// </summary>
    /// <param name="collision">The collider object that triggered the event.</param>

    public void OnTriggerEnter(Collider collision){
        if(!_isLoaded){
            return;
        }
        Debug.Log("Triggered");
        if(collision.gameObject.TryGetComponent<IElementScoreInteraction>(out IElementScoreInteraction BallElement)){

            //get the multiplier from the lookup table
            float multiplier = _currentScoreManager.elementMultiplierTable.GetMultiplier(_objectElement, BallElement.getElementType());
            Debug.Log($"Score Area Element: {_objectElement}, Target Element: {BallElement.getElementType()}, Multiplier: {multiplier}");

            //update the score
            float score = _currentScoreManager.UpdateMoneyWithMultiplier(_baseMultiplier * multiplier);

            //add the score record
            _currentScoreManager.UpdateElementMultiplierRecord(BallElement.getElementType(), score);

            //play the score effect
            if(collision.gameObject.TryGetComponent<IElementScoreInteractionVFX>(out IElementScoreInteractionVFX effect)){
                effect.OnScoreInteractionEffect();
            }

            //destroy the ball
            Destroy(collision.gameObject);
        }
    }

    //methods to add and remove element multipliers
    public void AddElementMultiplier(ElementMultiplier elementMultiplier){
        if (elementMultiplier.elementType != _objectElement)
        {
            _baseMultiplier += elementMultiplier.multiplier;
        }
       
    }
    //method to remove an element multiplier
    public void RemoveElementMultiplier(ElementMultiplier elementMultiplier){
        if (elementMultiplier.elementType != _objectElement)
        {
            _baseMultiplier -= elementMultiplier.multiplier;
        }
    }
    
    //implementing the interface methods
    public float getBaseMutiplier()
    {
        return _baseMultiplier;
    }

    public ElementClass.ElementType getElementType()
    {
        return _objectElement;
    }
    //not implemented

    public float getCurrentBid()
    {
        throw new System.NotImplementedException();
    }

    public float setCurrentBid(float bidValue)
    {
        throw new System.NotImplementedException();
    }

}
