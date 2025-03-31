using UnityEngine;

[CreateAssetMenu(fileName = "BoosterPackItem", menuName = "Scriptable Objects/BoosterPackItem")]
public abstract class BoosterPackItem : ScriptableObject
{
    public string itemName;

    public abstract void PerformAction();
}