using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
[RequireComponent(typeof(CardLogic))]
public class CardVisualEffect : MonoBehaviour
{

    private CardLogic _cardLogic => GetComponent<CardLogic>();

    private Animator _animator => GetComponent<Animator>();
    private Vector3 _originalRotation;
    private Sequence _sequence;

    private void Awake()
    {
        _originalRotation = transform.localEulerAngles;
    }
    private void Start()
    {
        _cardLogic.OnCardSelectedEvent += TriggerRotationEffect;
    }

    void TriggerRotationEffect(bool isStart)
    {
        print("TriggerRotationEffect: " + isStart);
        if(isStart)
        {
            StartRotationEffect();
        }
        else
        {
            StopRotationEffect();
        }
    }

    void StartRotationEffect()
    {
        _animator.SetBool("isCardRotating", true);
        // print("StartRotationEffect: " + _rotationAngle);
        // _sequence = DOTween.Sequence();
        // _sequence.Append(transform.DORotate(new Vector3(0, 0, _rotationAngle), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1));
        // _sequence.Play();
    }

    void StopRotationEffect()
    {
        _animator.SetBool("isCardRotating", false);
        // print("StopRotationEffect: " + _rotationAngle);
        // _sequence.Kill();
        // transform.DORotate(_originalRotation, 0.5f).SetEase(Ease.OutBack);
    }
}
