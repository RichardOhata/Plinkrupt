using UnityEngine;

public class BaseBallController : MonoBehaviour, IElementScoreInteraction
{
    public BallConfigSO ballConfig;

    private ScoringClass.ElementType ObjectElement;
    private float BaseMultiplier;

    public float getBaseMutiplier()
    {
        return BaseMultiplier;
    }

    public ScoringClass.ElementType getElementType()
    {
        return ObjectElement;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!ballConfig){
            Debug.LogWarning("Ball Config So is not set!");
            return;
        }

        ObjectElement = ballConfig.elementType;
        BaseMultiplier = ballConfig.baseScoreMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
