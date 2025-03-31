using UnityEngine;

[CreateAssetMenu(fileName = "ElementPack", menuName = "Scriptable Objects/ElementPack")]
public class ElementPack : BoosterPackItem
{
    public ElementClass.ElementType elementType; // Name of element
    public float multiplierInc; // Value to increment the multiplier by
    public Color color;
    public void IncrementMult()
    {
       Debug.Log(elementType.ToString());
       ElementMultiplierManager.Instance.OnMultiplierAddedEvent(elementType, multiplierInc);
    }

    public override void PerformAction()
    {
        ElementMultiplierManager.Instance.OnMultiplierAddedEvent(elementType, multiplierInc);
    }
}
