using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ElementMultiplierTable", menuName = "ScoringSystem/ElementMultiplierTable")]
public class ElementMultiplierTable : ScriptableObject
{
    
    public ElementTable[] elementTables = new ElementTable[0];
    /// <summary>
    /// Returns the multiplier for scoring the given target element relative to the given main element from the lookup table
    /// </summary>
    /// <param name="mainElement">The type of element that was used to score the target element.</param>
    /// <param name="targetElement">The type of element that was scored.</param>
    /// <returns>The multiplier for the target element, relative to the main element.</returns>
    public float GetMultiplier(ElementClass.ElementType mainElement, ElementClass.ElementType targetElement){
        return elementTables.FirstOrDefault(x => x.mainElement == mainElement).relativeElementsMultiplier.FirstOrDefault(x => x.elementType == targetElement).multiplier;
    }
    /// <summary>
    /// Returns the color associated with the given element type.
    /// </summary>
    /// <param name="elementType">The type of element for which to retrieve the color.</param>
    /// <returns>The color associated with the given element type.</returns>
    public Color GetElementColor(ElementClass.ElementType elementType){
        return elementTables.FirstOrDefault(x => x.mainElement == elementType).elementColor;
    }
}
