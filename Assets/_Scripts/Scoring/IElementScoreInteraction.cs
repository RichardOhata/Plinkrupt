using UnityEngine;

public interface IElementScoreInteraction
{

    // Interface methods
    public float getBaseMutiplier(); // Get the base multiplier
    public ElementClass.ElementType getElementType(); // Get the element type

    public float getCurrentBid(); // Get the current bid
    public float setCurrentBid(float bidValue); // Set the current bid
    public void DestorySelf();
}

public class ElementMultiplierConfig{
    public ElementClass.ElementType elementType; // The element type
     public float multiplier; // The multiplier
    public Color elementColor; // The element color
    public ElementMultiplierConfig(ElementClass.ElementType elementType, float multiplier, Color elementColor) {
        this.elementType = elementType;
        this.multiplier = multiplier;
        this.elementColor = elementColor;
    }
}
