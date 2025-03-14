using UnityEngine;
using static ShopLogic;

public class ConsumableConfig : MonoBehaviour
{

    public ConsumableType consumableType;
    public CardPos cardpos;

    public void SetConsumableType(ConsumableType type)
    {
        consumableType = type;
    }
    
    public ConsumableType GetConsumableType()
    {
        return consumableType;
    }
}
