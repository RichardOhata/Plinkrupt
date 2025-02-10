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
    public float GetMultiplier(ScoringClass.ElementType mainElement, ScoringClass.ElementType targetElement){
        return elementTables.FirstOrDefault(x => x.mainElement == mainElement).relativeElementsMultiplier.FirstOrDefault(x => x.elementType == targetElement).multiplier;
        
    }
}
