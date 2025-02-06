using UnityEngine;

[CreateAssetMenu(fileName = "ElementPack", menuName = "Scriptable Objects/ElementPack")]
public class ElementPack : ScriptableObject
{
    public string elementType; // Name of element
    public float multiplierInc; // Value to increment the multiplier by

    public void IncrementMult()
    {
       
    }
}
