using System;
using UnityEngine;

public class ScoringClass
{
    public enum ElementType
    {
        Air,
        Electric,
        Fire,
        Water,
        Holy,
    }
}

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

    public ElementMultiplier(ScoringClass.ElementType elementType, float multiplier){
        this.elementType = elementType;
        this.multiplier = multiplier;
    }
}

