using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;

public class EndOfRoundScoring : MonoBehaviour
{
    public GameObject bonusPrefab;
    public Transform bonusScrollViewContent; 
    public TMP_Text payout;
    public TMP_Text totalBonus;
    private GameManager _currentGameManager;

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
    void Start()
    {

        //bonusScoring = _currentGameManager.bonusScoring;

        ////TOD0: This data is for testing, remove after
        //float Score = 0f;
        //fillListWithTestData();

        //foreach(var scores in _scoreManager.StoredScoringRecord){
        //    Debug.Log($"Score: {scores.elementType}, {scores.score}");
        //    GameObject newItem = Instantiate(bonusPrefab, bonusScrollViewContent);
        //    TMP_Text textComponent = newItem.GetComponentInChildren<TMP_Text>();
        //    textComponent.text = $"{scores.elementType} {scores.score}";
        //    Score += scores.score;
        //}

        //// foreach (BonusScore bonus in bonusScoring.bonuses)
        //// {
        ////     GameObject newItem = Instantiate(bonusPrefab, bonusScrollViewContent);
        ////     TMP_Text textComponent = newItem.GetComponentInChildren<TMP_Text>();
        ////     textComponent.text = bonus.ToString();
        //// }

        //// Debug.Log($"Total Bonus: ${bonusScoring.getTotalBonus()}");
        //// payout.text = $"Payout: $XXXXX";
        //totalBonus.text = $"Total: ${Score}";
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
        float Score = _scoreManager.StoredScoringRecord.ToList().Sum(s => s.score);

        yield return new WaitForSeconds(delay);
        payout.text = $"Payout: ${Score}";
        StartCoroutine(bounceText(payout.gameObject));

        foreach(var scores in _scoreManager.StoredScoringRecord){
            yield return new WaitForSeconds(delay);
            Debug.Log($"Score: {scores.elementType}, {scores.score}");
            GameObject newItem = Instantiate(bonusPrefab, bonusScrollViewContent);
            TMP_Text textComponent = newItem.GetComponentInChildren<TMP_Text>();
            textComponent.text = $"{scores.elementType} {scores.score}";
            StartCoroutine(bounceText(newItem));
        }

        yield return new WaitForSeconds(delay);
        totalBonus.text = $"Total: ${_scoreManager.currentMoney}";
        StartCoroutine(bounceText(totalBonus.gameObject));
    }

    IEnumerator bounceText(GameObject textObject)
    {
        Vector3 originalScale = textObject.transform.localScale;

        // Shrink effect
        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            textObject.transform.localScale = Vector3.Lerp(originalScale, originalScale * 0.8f, t / 0.1f);
            yield return null;
        }

        // Expand with bounce
        for (float t = 0; t < 0.2f; t += Time.deltaTime)
        {
            float bounce = Mathf.Sin(t / 0.2f * Mathf.PI); // Simulates a bounce effect
            textObject.transform.localScale = originalScale * (1 + bounce * 0.2f);
            yield return null;
        }

        textObject.transform.localScale = originalScale; // Reset scale
    }

    void fillListWithTestData()
    {
        bonusScoring.addBonusByName("Electric Hit", 100);
        bonusScoring.addBonusByName("Electric Hit", 100);
        bonusScoring.addBonusByName("Electric Hit", 100);
        bonusScoring.addBonusByName("Electric Hit", 100);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Fire Hit", 200);
        bonusScoring.addBonusByName("Holy Hit", 500);
        bonusScoring.addBonusByName("Holy Hit", 500);
        bonusScoring.addBonusByName("Holy Hit", 500);
    }
}
