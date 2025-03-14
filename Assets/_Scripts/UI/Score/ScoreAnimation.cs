using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;

[RequireComponent(typeof(TMP_Text))]
public class ScoreAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Score Text Setting")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _duration = 0.5f;
    // [SerializeField] private int _startValue = 0;
    // [SerializeField] private int _endValue = 100;

    
    //render queue
    private Queue<int> _textQueueIndices;

    //timer
    private float _timer = 0f;

    void Awake()
    {
        _textQueueIndices = new Queue<int>();
        _scoreText = GetComponent<TMP_Text>();
    }
    void Start()
    {
        _scoreText.ForceMeshUpdate();
        //add character to queue

        for(int i = 0; i < _scoreText.textInfo.characterCount; i++){
            if(_scoreText.textInfo.characterInfo[i].isVisible){
                _textQueueIndices.Enqueue(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer < 0f){
            Debug.Log(_textQueueIndices.Count);
            if(_textQueueIndices.Count > 0){
                _timer = _duration;
                Debug.Log("Doing");
                int charIndex = _textQueueIndices.Dequeue();
                AnimateCharacter(charIndex);
            }
        }
        _timer -= Time.deltaTime;
    }

    private void AnimateCharacter(int charIndex){
        TMP_TextInfo charInfo = _scoreText.textInfo;

        //overcount
        // if(charIndex >= charInfo.characterCount || !charInfo.characterInfo[charIndex].isVisible){
        //     return;
        // }

        TMP_MeshInfo meshInfo = charInfo.meshInfo[charInfo.characterInfo[charIndex].materialReferenceIndex];
        int vertexIndex = charInfo.characterInfo[charIndex].vertexIndex;

        Vector3[] sourceVertices = meshInfo.vertices;

        DOTween.To(() => 1f, scale =>{
            for(int i = 0; i < 4; i++){
                Vector3 center = (sourceVertices[vertexIndex] + sourceVertices[vertexIndex + 2]) * 0.5f;
                Vector3 offset = sourceVertices[vertexIndex + i] - center;
                meshInfo.vertices[vertexIndex + i] = center + offset * scale;
            }
            _scoreText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        },1.1f, _duration).SetEase(Ease.InOutSine);
    }
}
