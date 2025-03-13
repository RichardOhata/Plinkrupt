using System;
using UnityEngine;
using UnityEngine.VFX;


[RequireComponent(typeof(BaseBallController))]
public class BallEffectController : MonoBehaviour, IElementScoreInteractionVFX
{
    [Header("Ball Effect Settings")]
    [SerializeField] private VisualEffect _ImpactEffect;
    //this is a game object
    [SerializeField] private GameObject _ScoreEffectObject;
    [SerializeField] private float _oneShotCooldown = 0.5f;
    private BaseBallController _ballController;

    private float _timer = 0f;
    void Awake()
    {
        if(!_ImpactEffect){
            _ImpactEffect = FindFirstObjectByType<VisualEffect>();
        }            
        _ImpactEffect.Stop();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ballController = GetComponent<BaseBallController>();
        _ballController.OnBallCollided += PlayImpactEffect;
    }

    private void PlayImpactEffect()
    {
        if(_timer <= 0){
            GameObject impactEffect = Instantiate(_ImpactEffect.gameObject, transform.position, Quaternion.identity);
            impactEffect.GetComponent<VisualEffect>().Play();
            _timer = _oneShotCooldown;
            Destroy(impactEffect, 3f);
        }
    }

    void Update()
    {
        if(_timer > 0){
            _timer -= Time.deltaTime;
        }
    }

    public void OnScoreInteractionEffect()
    {
        GameObject scoreEffect = Instantiate(_ScoreEffectObject, transform.position, Quaternion.identity);
        Destroy(scoreEffect, 3f);
    }
}
