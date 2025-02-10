using UnityEngine;

public interface IElementScoreInteraction
{
    public float getBaseMutiplier();
    public ScoringClass.ElementType getElementType();

    public float getCurrentBid();
    public float setCurrentBid(float bidValue);
    
}
