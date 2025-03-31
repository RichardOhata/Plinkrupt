using UnityEngine;

[CreateAssetMenu(fileName = "ElementPack", menuName = "Scriptable Objects/ElementPack")]
public class ElementPack : BoosterPackItem
{
    public ElementClass.ElementType elementType; // Name of element
    public float multiplierInc; // Value to increment the multiplier by
    public Color color;
    public override void PerformAction()
    {
        ElementMultiplierManager.Instance.OnMultiplierAddedEvent(elementType, multiplierInc);
    }

    private float GetCurrentMultiplier(ElementClass.ElementType element)
    {
        // Search through permanent multipliers to find the matching element
        ElementMultiplier currentMultiplier = ElementMultiplierManager.Instance.premanentMultipliers
            .Find(multiplier => multiplier.elementType == element);

        // If not found, return 1.0 (default multiplier)
        if (currentMultiplier != null)
        {
            return currentMultiplier.multiplier;
        }

        return 1.0f; // Default multiplier if the element is not found
    }

    public override string Description =>
      $"Increases <color=#{ColorUtility.ToHtmlStringRGB(color)}>{elementType}</color> Element Mult by {multiplierInc}x \n(Current Total Mult: {GetCurrentMultiplier(this.elementType)})";

}
