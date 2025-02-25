using UnityEngine;

public class BonusScore
{
    public string name;
    public int value;
    public int counter;

    // Constructor
    public BonusScore(string name, int value, int counter)
    {
        this.name = name;
        this.value = value;
        this.counter = counter;
    }
    public override string ToString()
    {
        return $"{name} x {counter} = ${value * counter} ({value} x {counter})";
    }

    public int getTotalBonus()
    {
        return value * counter;
    }
}
