using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "BallConfigSO", menuName = "BallSo/Ball Config")]
public class BallConfigSO : ScriptableObject
{
    public ScoringClass.ElementType elementType;
    public float baseScoreMultiplier;

    public ParticleSystem tailParticle;
    public VisualEffectAsset hitVFX;

}
