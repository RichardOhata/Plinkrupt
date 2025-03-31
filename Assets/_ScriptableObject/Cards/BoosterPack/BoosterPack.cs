using UnityEngine;

[CreateAssetMenu(fileName = "BoosterPack", menuName = "Scriptable Objects/BoosterPack")]
public class BoosterPack : ScriptableObject
{

    public GameObject boosterPackPrefab;

    [Header("Contained Items In Booster Pack")]
    public BoosterPackItem[] itemPool;

    [Header("Contained Items In Booster Pack Card Prefab")]
    public GameObject cardPrefab;

    public string description;
}
