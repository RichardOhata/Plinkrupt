using Unity.VisualScripting;
using UnityEngine;
using static ShopLogic;

public class ConsumableConfig : MonoBehaviour
{
    public ConsumableType consumableType;
    public CardPos cardpos;
    public string description;
    public void SetConsumableType(ConsumableType type)
    {
        consumableType = type;
    }
    
    public ConsumableType GetConsumableType()
    {
        return consumableType;
    }

    public void SetDescription(string desc)
    {
        description = desc;
    }

    public string GetDescription()
    {
        return description;
    }
}
