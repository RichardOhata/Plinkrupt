using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Rendering.Universal.Internal;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using System;


[RequireComponent(typeof(TMP_Text))]
public class ScoreAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Score Text Setting")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _transitionDuration = 0.5f;
    [SerializeField] private float _maxScale = 1.5f;

    [Header("Character Queue Setting")]
    //text queue and logic
    private Queue<int> _textQueueIndices;
    [SerializeField] private int maxValueInQueue = 20;
    //timer
    private float _timer = 0f;

    [Header("Color Animation Setting")]
    //Color animation Setting
    [SerializeField] private float _colorTransitionDuration = 0.25f;
    private bool _isColorAnimationEnabled = false;

    void Awake()
    {
        _textQueueIndices = new Queue<int>();
        _scoreText = GetComponent<TMP_Text>();

        //Trigger event when score updated
        ScoreManager.Instance.OnScoreUpdatedEvent += UpdateScore;
        ScoreManager.Instance.OnLeadingElementChangedEvent += OnLeadingElementChange;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreUpdatedEvent -= UpdateScore;
        ScoreManager.Instance.OnLeadingElementChangedEvent -= OnLeadingElementChange;
    }

    private void OnLeadingElementChange((Color, float) tuple)
    {   
        //Change color if there is a new leading element
        //and the current animation is not running
        if(!_isColorAnimationEnabled){
            _isColorAnimationEnabled = true;
            _scoreText.DOColor(tuple.Item1, _colorTransitionDuration).SetEase(Ease.OutSine).OnComplete(() => _isColorAnimationEnabled = false);
        }
    }

    void Start()
    {
        // _scoreText.ForceMeshUpdate();
        // //add character to queue
        // QueueAllCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer < 0f){
            if(_textQueueIndices.Count > 0){
                // Debug.Log(_textQueueIndices.Count);
                _timer = _transitionDuration;
                int charIndex = _textQueueIndices.Dequeue();
                AnimateCharacter(charIndex);
            }
        }
        _timer -= Time.deltaTime;
    }
    private void AnimateCharacter(int charIndex){
        TMP_TextInfo charInfo = _scoreText.textInfo;

        //overcount
        if(charIndex >= charInfo.characterCount || !charInfo.characterInfo[charIndex].isVisible){
            return;
        }

        int meshIndex = charInfo.characterInfo[charIndex].materialReferenceIndex;
        int vertexIndex = charInfo.characterInfo[charIndex].vertexIndex;
        Vector3[] vertices = charInfo.meshInfo[meshIndex].vertices;

        Vector3 charCenter = (vertices[vertexIndex] + vertices[vertexIndex + 2]) * 0.5f;

        Vector3[] originalPositions = new Vector3[4];

        for (int i = 0; i < 4; i++) {
            originalPositions[i] = vertices[vertexIndex + i];
        }

        //modify vertices position with scale
        float initialScale = 1f;

        //tween scale from 1f to _scale back to 1f
        DOTween.To(() => initialScale, scale =>{
            for(int i = 0; i < 4; i++){
                vertices[vertexIndex + i] = (originalPositions[i] - charCenter) * scale + charCenter;
            }
            _scoreText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }, _maxScale, _transitionDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void UpdateScore(float score){
        _scoreText.text = score.ToString();
        _scoreText.ForceMeshUpdate(true, true);
        //add character to queue
        QueueAllCharacter();
    }

    private void QueueAllCharacter(){
        //add character to queue
        for(int i = 0; i < _scoreText.textInfo.characterCount; i++){
            if(_scoreText.textInfo.characterInfo[i].isVisible){
                if(_textQueueIndices.Count >= maxValueInQueue){
                    return;
                }
                _textQueueIndices.Enqueue(i);
            }
        }
    }
}
