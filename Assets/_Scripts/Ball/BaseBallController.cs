using UnityEngine;

public class BaseBallController : MonoBehaviour, IElementScoreInteraction
{
    public BallConfigSO ballConfig;

    private ScoringClass.ElementType _objectElement;
    private float _baseMultiplier;

    private float _currentBidValue;

    // Implementing the interface methods
    public float getBaseMutiplier()
    {
        return _baseMultiplier;
    }

    public float getCurrentBid()
    {
        return _currentBidValue;
    }
    public float setCurrentBid(float bidValue)
    {
        _currentBidValue = bidValue;
        return _currentBidValue;
    }

    public ScoringClass.ElementType getElementType()
    {
        return _objectElement;
    }

    //
    // Load Ball Config SO
    void Start()
    {
        if(!ballConfig){
            Debug.LogWarning("Ball Config So is not set!");
            return;
        }

        _objectElement = ballConfig.elementType;
        _baseMultiplier = ballConfig.baseScoreMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
