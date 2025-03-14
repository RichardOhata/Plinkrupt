
using UnityEngine;

[CreateAssetMenu(fileName = "ScoringTriggerSo", menuName = "Scoring Area/Scoring Area Config")]
public class ScoringAreaConfigSO : ScriptableObject
{
    public ScoringClass.ElementType elementType;
    public float baseScoreMultiplier;
    public Color ScoreAreaColor;

    public ElementMultiplierConfig getScoringAreaConfig()
    {
        return new ElementMultiplierConfig(elementType, baseScoreMultiplier, ScoreAreaColor);
    }

    public Color getColor()
    {
        return ScoreAreaColor;
    }
}
