using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using DG.Tweening;

public class EndOfRoundScoring : MonoBehaviour
{
    public GameObject bonusPrefab;
    public Transform bonusScrollViewContent; 
    public TMP_Text payout;
    public TMP_Text totalBonus;
    private GameManager _currentGameManager;
    public AudioSource cashoutSound;

    private ScoreManager _scoreManager;
    private BonusScoring bonusScoring;

    void OnEnable()
    {
        _scoreManager = ScoreManager.Instance;
        if(_scoreManager == null){
            Debug.LogWarning("Score Manager is not set!");
            return;
        }
        _currentGameManager = GameManager.Instance;
        if(!GameManager.Instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        FillData();
    }

    private void OnDisable()
    {
        _scoreManager.StoredScoringRecord.Clear();
    }

    private void FillData()
    {
        foreach (Transform child in bonusScrollViewContent)
        {
            Destroy(child.gameObject);
        }
        bonusScoring = _currentGameManager.bonusScoring;
        totalBonus.text = "";
        StartCoroutine(displayBonus(0.25f));
    }

    IEnumerator displayBonus(float delay)
    {
        cashoutSound.PlayOneShot(cashoutSound.clip, 0.75f);

        float Score = _scoreManager.StoredScoringRecord.ToList().Sum(s => s.score);

        yield return new WaitForSeconds(delay);
        payout.text = $"Payout: ${Score}";
        BounceText(payout.gameObject);

        foreach(var scores in _scoreManager.StoredScoringRecord){
            yield return new WaitForSeconds(delay);
            Debug.Log($"Score: {scores.elementType}, {scores.score}");
            GameObject newItem = Instantiate(bonusPrefab, bonusScrollViewContent);
            TMP_Text textComponent = newItem.GetComponentInChildren<TMP_Text>();
            textComponent.text = $"{scores.elementType} {scores.score}";
            BounceText(newItem);
        }

        yield return new WaitForSeconds(delay);
        totalBonus.text = $"Total: ${_scoreManager.currentMoney}";
        BounceText(totalBonus.gameObject);
    }

    void BounceText(GameObject textObject)
    {
        // Store the original scale
        Vector3 originalScale = textObject.transform.localScale;

        // Create a sequence for the bounce animation
        Sequence bounceSequence = DOTween.Sequence();

        // First shrink
        bounceSequence.Append(textObject.transform.DOScale(originalScale * 0.8f, 0.1f))
                     // Then bounce back with overshoot
                     .Append(textObject.transform.DOScale(originalScale * 1.2f, 0.2f).SetEase(Ease.OutBounce))
                     // Finally return to original scale
                     .Append(textObject.transform.DOScale(originalScale, 0.1f));

        // Play the sequence
        bounceSequence.Play();
    }
}
