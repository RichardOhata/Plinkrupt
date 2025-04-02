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
    private ElementMultiplierManager _elementMultiplierManager;
    private GameManager _gameManager;

    //configs
    private ScoringAreaConfigSO _scoringAreaConfigSo;
    private ElementClass.ElementType _objectElement;
    private Color _elementColor;
    private float _baseMultiplier;

    public event Action<Color> OnScoreAreaVisualChangeEvent;
    public ScoringAreaConfigSO[] scoringAreaConfigSOs;

    void OnEnable(){
        _currentScoreManager = ScoreManager.Instance;
        _elementMultiplierManager = ElementMultiplierManager.Instance;
        _gameManager = GameManager.Instance;
    }
    void Start()
    {
        if(!ScoreManager.Instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        OnScoreAreaVisualChangeEvent?.Invoke(_elementColor);
        ElementMultiplierManager.Instance.MultiplierAddedEvent += AddElementMultiplier;
        ElementMultiplierManager.Instance.MultiplierRemovedEvent += RemoveElementMultiplier;
    }
    private void OnDestroy(){
        ElementMultiplierManager.Instance.MultiplierAddedEvent -= AddElementMultiplier;
        ElementMultiplierManager.Instance.MultiplierRemovedEvent -= RemoveElementMultiplier;
    }


    public void UpdateSetting(ElementMultiplierConfig scoringAreaConfig)
    {
        //set configs
        //_isLoaded = true;
        _objectElement = scoringAreaConfig.elementType;
        _baseMultiplier = scoringAreaConfig.multiplier;
        _elementColor = scoringAreaConfig.elementColor;

        OnScoreAreaVisualChangeEvent?.Invoke(_elementColor);
    }
    /// <summary>
    /// Handles the trigger enter event. When a collision occurs, it checks if the colliding object 
    /// implements the IElementScoreInteraction interface. If so, calculates the score using the 
    /// element multiplier and updates the game manager with the new score.
    /// </summary>
    /// <param name="collision">The collider object that triggered the event.</param>
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.TryGetComponent<IElementScoreInteraction>(out IElementScoreInteraction BallElement)){

            //get the multiplier from the lookup table
            float multiplier = _currentScoreManager.elementMultiplierTable.GetMultiplier(_objectElement, BallElement.getElementType());
            // Debug.Log($"Score Area Element: {_objectElement}, Target Element: {BallElement.getElementType()}, Multiplier: {multiplier}");
            float elementMultiplier = 0f;

            if(_objectElement == BallElement.getElementType()){
                elementMultiplier = _elementMultiplierManager.getElementMultiplier(BallElement.getElementType());
            }

            //update the score
            float score = _currentScoreManager.UpdateMoneyWithMultiplier(_baseMultiplier * multiplier + elementMultiplier);

            //add the score record
            _currentScoreManager.UpdateElementMultiplierRecord(BallElement.getElementType(), score);

            //play the score effect
            if(collision.gameObject.TryGetComponent<IElementScoreInteractionVFX>(out IElementScoreInteractionVFX effect)){
                effect.OnScoreInteractionEffect();
            }
            //destroy the ball
            BallElement.DestorySelf();
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
    public float getBaseMutiplier() => _baseMultiplier;
    public ElementClass.ElementType getElementType() => _objectElement;
    public float getCurrentBid() => throw new NotImplementedException();
    public float setCurrentBid(float bidValue) => throw new NotImplementedException();

    public void DestorySelf()
    {
        throw new NotImplementedException();
    }

    //implementing the builder pattern for the ScoringTriggerZone class
    public class Builder{
        private ScoringAreaConfigSO _scoringAreaConfigSo;
        private ElementClass.ElementType _objectElement;
        private Color _elementColor = Color.white; //default color
        private float _baseMultiplier = 1f; //default multiplier

        public Builder WithConfig(ScoringAreaConfigSO scoringAreaConfigSo){
            _scoringAreaConfigSo = scoringAreaConfigSo;
            return this;
        }
        public Builder WithElementType(ElementClass.ElementType elementType){
            _objectElement = elementType;
            return this;
        }

        public Builder WithElementColor(Color elementColor){
            _elementColor = elementColor;
            return this;

        }
        public Builder WithBaseMultiplier(float baseMultiplier){
            _baseMultiplier = baseMultiplier;
            return this;
        }

        public ScoringTriggerZone Build(GameObject gameObject){
            ScoringTriggerZone scoringTriggerZone = gameObject.AddComponent<ScoringTriggerZone>();

            //if the scoring area config so is not set, use the builder values
            if(_scoringAreaConfigSo == null){
                scoringTriggerZone._objectElement = _objectElement;
                scoringTriggerZone._elementColor = _elementColor;
                scoringTriggerZone._baseMultiplier = _baseMultiplier;
            }

            //if the scoring area config so is set
            else{
                scoringTriggerZone._scoringAreaConfigSo = _scoringAreaConfigSo;
                scoringTriggerZone._objectElement = _scoringAreaConfigSo.getScoringAreaConfig().elementType;
                scoringTriggerZone._elementColor = _scoringAreaConfigSo.getScoringAreaConfig().elementColor; 
                scoringTriggerZone._baseMultiplier = _scoringAreaConfigSo.getScoringAreaConfig().multiplier;
            }

            gameObject.AddComponent<ScoringAreaVisualModifier>();
            scoringTriggerZone.OnScoreAreaVisualChangeEvent?.Invoke(scoringTriggerZone._elementColor);
            
            return scoringTriggerZone;
        }
    }
}
