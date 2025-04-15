using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "BallConfigSO", menuName = "BallSo/Ball Config")]
public class BallConfigSO : ScriptableObject
{   
    [Header("Ball Config Settings")]
    public GameObject ballVisualPrefab;
    public ElementClass.ElementType elementType;
    public float baseScoreMultiplier;
    
    [Header("VFX Effect Setting")]
    public ParticleSystem tailParticle;
    public GameObject hitVFXPrefab;
    public float _hitEffectInitialSize = 0.5f;
    public GameObject explosionVFXPrefab;
    [Range(0,5)]
    public float _oneShotCooldown = 0.5f;

    [Header("SFX Settings")]
    public AudioSource onBounce;
    public AudioSource onDestroy;
}
