using UnityEngine;

public interface IElementScoreInteraction
{

    // Interface methods
    public float getBaseMutiplier(); // Get the base multiplier
    public ScoringClass.ElementType getElementType(); // Get the element type

    public float getCurrentBid(); // Get the current bid
    public float setCurrentBid(float bidValue); // Set the current bid

    // Load Setting 
    public void LoadSetting(ElementMultiplierConfig elementMultiplierConfig);
}

public class ElementMultiplierConfig{
    public ScoringClass.ElementType elementType; // The element type
     public float multiplier; // The multiplier
    public Color elementColor; // The element color
    public ElementMultiplierConfig(ScoringClass.ElementType elementType, float multiplier, Color elementColor) {
        this.elementType = elementType;
        this.multiplier = multiplier;
        this.elementColor = elementColor;
    }
}
