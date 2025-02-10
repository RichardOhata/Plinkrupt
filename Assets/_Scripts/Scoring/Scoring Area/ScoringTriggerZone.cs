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
        if(!GameManager.instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        _currentGameManager = GameManager.instance;
        _objectElement = _scoringAreaConfigSo.elementType;
        _baseMultiplier = _scoringAreaConfigSo.baseScoreMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
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
