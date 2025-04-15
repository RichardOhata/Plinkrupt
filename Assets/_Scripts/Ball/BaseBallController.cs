using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class BaseBallController : MonoBehaviour, IElementScoreInteraction
{
    public BallConfigSO ballConfig;
    private ElementClass.ElementType _objectElement;
    private AudioSource _onBounce;
    private AudioSource _onDestroy;
    private float _baseMultiplier;
    private float _currentBidValue;
    private GameManager _gameManager;

    [SerializeField] private VisualEffect _ImpactEffect;
    private VisualEffect _explosionEffect;
    public VisualEffect ImpactEffect { get => _ImpactEffect; set => _ImpactEffect = value; }

    [HideInInspector] public bool isBeingCleared = false;

    private bool isDestroyed = false;
    public event Action OnBallCollided;

    // Implementing the interface methods
    public float getBaseMutiplier()
    {
        return _baseMultiplier;
    }

    public float getCurrentBid()
    {
        return _currentBidValue;
    }
    public float setCurrentBid(float bidValue)
    {
        _currentBidValue = bidValue;
        return _currentBidValue;
    }

    public ElementClass.ElementType getElementType()
    {
        return _objectElement;
    }

    void OnEnable()
    {
        //get the game manager instance
        _gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //should not collide with itself and the ball
        if (collision.gameObject.CompareTag(gameObject.tag)){
            return;
        }

        _onBounce.PlayOneShot(_onBounce.clip);

        OnBallCollided?.Invoke();
    }

    public void DestorySelf()
    {
        //prevent double destruction
        if(isDestroyed) return;


        isDestroyed = true;
        //update the ball count in the game manager and destroy the ball
        _gameManager.currentBallInstances -= 1;
        Debug.Log("Destroying ball: " + gameObject.name);
        Debug.Log("Ball count: " + _gameManager.currentBallInstances);
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if(_onDestroy != null && !isBeingCleared)
        {
            GameObject audioGO = new GameObject("DestructionSound");
            audioGO.hideFlags = HideFlags.DontSave;
            AudioSource audioSource = audioGO.AddComponent<AudioSource>();
            audioSource.clip = _onDestroy.clip;
            audioSource.volume = 0.6f;
            audioSource.Play();

            // Destroy the temporary GameObject after the clip finishes
            Destroy(audioGO, _onDestroy.clip.length);
        }
    }

    //builder pattern for the BaseBallController class
    public class Builder{
        private BallConfigSO _ballConfigSo;
        private float _currentBid;
        public Builder WithConfig(BallConfigSO ballConfigSo){
            _ballConfigSo = ballConfigSo;
            return this;
        }
        public Builder WithCurrentBid(float currentBid){
            _currentBid = currentBid;
            return this;
        }

        public BaseBallController Build(GameObject gameObject){
            BaseBallController baseBallController = gameObject.AddComponent<BaseBallController>();

            //set the ball config so
            baseBallController.ballConfig = _ballConfigSo;
            baseBallController._objectElement = baseBallController.ballConfig.elementType;
            baseBallController._baseMultiplier = baseBallController.ballConfig.baseScoreMultiplier;
            baseBallController._currentBidValue = _currentBid;

            //Inistantiate the Audio source and set the clip
            baseBallController._onBounce = Instantiate(baseBallController.ballConfig.onBounce, gameObject.transform.position, Quaternion.identity);
            baseBallController._onBounce.transform.SetParent(gameObject.transform);

            baseBallController._onDestroy = Instantiate(baseBallController.ballConfig.onDestroy, gameObject.transform.position, Quaternion.identity);
            baseBallController._onDestroy.transform.SetParent(gameObject.transform);

            float multiplier = ElementMultiplierManager.Instance.getElementMultiplier(baseBallController._objectElement);

            //set the Impact effect
            GameObject ballVFXObject = Instantiate(baseBallController.ballConfig.hitVFXPrefab, gameObject.transform.position, Quaternion.identity);
            ballVFXObject.transform.SetParent(gameObject.transform);
            baseBallController.ImpactEffect = ballVFXObject.GetComponent<VisualEffect>();
            baseBallController.ImpactEffect.SetFloat("Multiplier", multiplier * baseBallController.ballConfig._hitEffectInitialSize);

            //set the explosion effect
            GameObject explosionVFXObject = Instantiate(baseBallController.ballConfig.explosionVFXPrefab, gameObject.transform.position, Quaternion.identity);
            explosionVFXObject.transform.SetParent(gameObject.transform);
            baseBallController._explosionEffect = explosionVFXObject.GetComponent<VisualEffect>();

            //build the ball visual prefab
            if(baseBallController.ballConfig.ballVisualPrefab){
                GameObject ballVisualPrefab = Instantiate(baseBallController.ballConfig.ballVisualPrefab, gameObject.transform.position, Quaternion.identity);
                ballVisualPrefab.transform.SetParent(gameObject.transform);
                ballVisualPrefab.transform.localScale = Vector3.one * multiplier * baseBallController.ballConfig._hitEffectInitialSize;
            }




            //stop the effects
            baseBallController._explosionEffect.Stop();
            baseBallController.ImpactEffect.Stop();
            
            //builder the effect controller
            BallEffectController ballEffectController = new BallEffectController.Builder()
                .WithImpactEffect(baseBallController.ImpactEffect)
                .WithScoreEffectObject(baseBallController._explosionEffect)
                .WithOneShotCooldown(baseBallController.ballConfig._oneShotCooldown)
                .WithBallController(baseBallController)
                .Build(gameObject);

            return baseBallController;
        }
    }
}
