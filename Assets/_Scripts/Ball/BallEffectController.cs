using System;
using UnityEngine;
using UnityEngine.VFX;


[RequireComponent(typeof(BaseBallController))]
public class BallEffectController : MonoBehaviour, IElementScoreInteractionVFX
{
    [Header("Ball Effect Settings")]
    [SerializeField] private VisualEffect _impactEffect;
    //this is a game object
    [SerializeField] private VisualEffect _scoreEffectObject;
    [SerializeField] private float _oneShotCooldown = 0.5f;
    private BaseBallController _ballController;

    private float _timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ballController = GetComponent<BaseBallController>();
        _ballController.OnBallCollided += PlayImpactEffect;
    }

    private void PlayImpactEffect()
    {
        //check if the ball is not in the cooldown period
        if(_timer <= 0){
            //add the impact effect
            GameObject impactEffect = Instantiate(_impactEffect.gameObject, transform.position, Quaternion.identity);
            impactEffect.GetComponent<VisualEffect>().Play();
            _timer = _oneShotCooldown;

            //destroy the impact effect after 3 seconds
            Destroy(impactEffect, 3f);
        }
    }

    void Update()
    {
        //update timer
        if(_timer > 0){
            _timer -= Time.deltaTime;
        }
    }

    public void OnScoreInteractionEffect()
    {
        //play the score effect and destroy it after 3 seconds
        GameObject scoreEffect = Instantiate(_scoreEffectObject.gameObject, transform.position, Quaternion.identity);
        Destroy(scoreEffect, 3f);
    }
    

    public class Builder{
        private VisualEffect _impactEffect;
        //this is a game object
        private VisualEffect _scoreEffectObject;
        private float _oneShotCooldown = 0.5f;
        private BaseBallController _ballController;


        /// Set the impact effect for the ball
        public Builder WithImpactEffect(VisualEffect impactEffect){
            _impactEffect = impactEffect;
            return this;
        }
        /// Set the score effect object for the ball
        public Builder WithScoreEffectObject(VisualEffect scoreEffectObject){
            _scoreEffectObject = scoreEffectObject;
            return this;
        }    
        /// Set the cooldown time between two consecutive impacts
        public Builder WithOneShotCooldown(float oneShotCooldown){
            _oneShotCooldown = oneShotCooldown;
            return this;
        }
        /// Set the ball controller for the ball
        public Builder WithBallController(BaseBallController ballController){
            _ballController = ballController;
            return this;
        }
        /// Build the BallEffectController
        public BallEffectController Build(GameObject gameObject){
            BallEffectController ballEffectController = gameObject.AddComponent<BallEffectController>();
            ballEffectController._impactEffect = _impactEffect;
            ballEffectController._scoreEffectObject = _scoreEffectObject;
            ballEffectController._oneShotCooldown = _oneShotCooldown;
            ballEffectController._ballController = _ballController;
            return ballEffectController;
        }
    }
    private void OnDestroy()
    {
        _ballController.OnBallCollided -= PlayImpactEffect;
    }
}
