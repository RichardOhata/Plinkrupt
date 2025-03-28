using UnityEngine;

[CreateAssetMenu(fileName = "BoosterPack", menuName = "Scriptable Objects/BoosterPack")]
public class BoosterPack : ScriptableObject
{

    public PackType type;
    public enum PackType
    {
        ElementalBoosterPack,
        OracleBoosterPack

    }

    public GameObject boosterPackPrefab;
}
