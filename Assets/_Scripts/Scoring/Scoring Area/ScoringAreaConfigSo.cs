using UnityEngine;

[CreateAssetMenu(fileName = "ScoringTriggerSo", menuName = "Scoring Area/Scoring Area Config")]
public class ScoringAreaConfigSO : ScriptableObject
{
    public ScoringClass.ElementType elementType;
    public float baseScoreMultiplier;

}
