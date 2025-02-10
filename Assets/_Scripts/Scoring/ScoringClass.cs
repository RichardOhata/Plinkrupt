using System;
using UnityEngine;

public class ScoringClass
{
    //Element types
    public enum ElementType
    {
        Air,
        Electric,
        Fire,
        Water,
        Holy,
    }
}

//Lookup table for scoring multipliers
[Serializable]
public class ElementTable{
    public ScoringClass.ElementType mainElement;
    public ElementMultiplier[] relativeElementsMultiplier;

    /// <summary>
    /// Constructor for ElementTable
    /// </summary>
    /// <param name="elementType">The main element type of the table</param>
    public ElementTable(ScoringClass.ElementType elementType){
        mainElement = elementType;
    }
    public ElementTable(ScoringClass.ElementType elementType, ElementMultiplier[] relativeElementsMultiplier){
        mainElement = elementType;
        this.relativeElementsMultiplier = relativeElementsMultiplier;
    }
}

[Serializable]
public class ElementMultiplier{
    public ScoringClass.ElementType elementType;
    public float multiplier;
    
    //optional name the keep track of buffs and debuffs
    //should be useful for Debug/UI purposes
    public String ElementBuffName;

    /// <summary>
    /// Constructor for ElementMultiplier
    /// </summary>
    /// <param name="elementType">The element type to have the given multiplier</param>
    /// <param name="multiplier">The value to multiply the score by when the main element of the table is used to score the target element.</param>
    public ElementMultiplier(ScoringClass.ElementType elementType, float multiplier){
        this.elementType = elementType;
        this.multiplier = multiplier;
        ElementBuffName = elementType.ToString();
    }
}

