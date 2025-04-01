
using UnityEngine;

[CreateAssetMenu(fileName = "ScoringTriggerSo", menuName = "Scoring Area/Scoring Area Config")]
public class ScoringAreaConfigSO : ScriptableObject
{
    public ElementClass.ElementType elementType;
    public float baseScoreMultiplier;
    public Color scoreAreaColor;

    public ElementMultiplierConfig getScoringAreaConfig()
    {
        return new ElementMultiplierConfig(elementType, baseScoreMultiplier, scoreAreaColor);
    }

    public Color getColor()
    {
        return scoreAreaColor;
    }
}
