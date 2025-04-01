using System;
using System.Collections.Generic;
using UnityEngine;

public class ElementClass
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

    // public Dictionary<ElementType>
}

//Lookup table for scoring multipliers
[Serializable]
public class ElementTable{
    public ElementClass.ElementType mainElement;
    public ElementMultiplier[] relativeElementsMultiplier;
    public Color elementColor;

    /// <summary>
    /// Constructor for ElementTable
    /// </summary>
    /// <param name="elementType">The main element type of the table</param>
    public ElementTable(ElementClass.ElementType elementType){
        mainElement = elementType;
    }
    public ElementTable(ElementClass.ElementType elementType, ElementMultiplier[] relativeElementsMultiplier){
        mainElement = elementType;
        this.relativeElementsMultiplier = relativeElementsMultiplier;
    }

    public ElementTable(ElementClass.ElementType elementType, ElementMultiplier[] relativeElementsMultiplier, Color elementColor){
        mainElement = elementType;
        this.relativeElementsMultiplier = relativeElementsMultiplier;
        this.elementColor = elementColor;
    }
}

//Store the Record of the score and element type
[Serializable]
public class ElementScoreRecord{
    public ElementClass.ElementType elementType;
    public float score;

    public ElementScoreRecord(ElementClass.ElementType elementType, float score){
        this.elementType = elementType;
        this.score = score;
    }
}

[Serializable]
public class ElementMultiplier{
    public ElementClass.ElementType elementType;
    public float multiplier;
    
    //optional name the keep track of buffs and debuffs
    //should be useful for Debug/UI purposes
    [HideInInspector]public String ElementBuffName;

    /// <summary>
    /// Constructor for ElementMultiplier
    /// </summary>
    /// <param name="elementType">The element type to have the given multiplier</param>
    /// <param name="multiplier">The value to multiply the score by when the main element of the table is used to score the target element.</param>
    public ElementMultiplier(ElementClass.ElementType elementType, float multiplier){
        this.elementType = elementType;
        this.multiplier = multiplier;
        ElementBuffName = elementType.ToString();
    }
}

