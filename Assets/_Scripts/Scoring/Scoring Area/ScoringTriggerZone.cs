using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class ScoringTriggerZone : MonoBehaviour, IElementScoreInteraction
{



    //cached game manager references
    private GameManager _currentGameManager;

    //configs
    [SerializeField]private ScoringAreaConfigSO _scoringAreaConfigSo;
    private ScoringClass.ElementType _objectElement;
    private float _baseMultiplier;
    


    void Start()
    {
        if(!_scoringAreaConfigSo){
            Debug.LogWarning("Scoring Trigger So is not set!");
            return;
        }
        if(!GameManager.Instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        _currentGameManager = GameManager.Instance;
        _objectElement = _scoringAreaConfigSo.elementType;
        _baseMultiplier = _scoringAreaConfigSo.baseScoreMultiplier;
        
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
        Debug.Log("Triggered");
        if(collision.gameObject.TryGetComponent<IElementScoreInteraction>(out IElementScoreInteraction BallElement)){

            //get the multiplier from the lookup table
            float multiplier = _currentGameManager.elementMultiplierTable.GetMultiplier(_objectElement, BallElement.getElementType());
            Debug.Log($"Score Area: {_objectElement}, Target Element: {BallElement.getElementType()}, Multiplier: {multiplier}");

            float bid = BallElement.getCurrentBid();
            //update the score
            float score = _currentGameManager.UpdateMoneyWithMultiplier(bid, _baseMultiplier * multiplier);
            
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

    public ScoringClass.ElementType getElementType()
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
