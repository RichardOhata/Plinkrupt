using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfRoundScoring : MonoBehaviour
{
    public GameObject bonusPrefab;
    public Transform bonusScrollViewContent; 
    public TMP_Text payout;
    public TMP_Text totalBonus;
    private GameManager _currentGameManager;
    private BonusScoring bonusScoring;

    void Start()
    {
        if(!GameManager.Instance){
            Debug.LogWarning("Game Manager is not set!");
            return;
        }
        _currentGameManager = GameManager.Instance;
        bonusScoring = _currentGameManager.bonusScoring;

        //TOD0: This data is for testing, remove after
        fillListWithTestData();

        foreach (BonusScore bonus in bonusScoring.bonuses)
        {
            GameObject newItem = Instantiate(bonusPrefab, bonusScrollViewContent);
            TMP_Text textComponent = newItem.GetComponentInChildren<TMP_Text>();
            textComponent.text = bonus.ToString();
        }

        Debug.Log($"Total Bonus: ${bonusScoring.getTotalBonus()}");
        payout.text = $"Payout: $XXXXX";
        totalBonus.text = $"Total Bonus: ${bonusScoring.getTotalBonus()}";
    }

    void fillListWithTestData()
    {
        bonusScoring.addBonuseByName("Electric Hit", 100);
        bonusScoring.addBonuseByName("Electric Hit", 100);
        bonusScoring.addBonuseByName("Electric Hit", 100);
        bonusScoring.addBonuseByName("Electric Hit", 100);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Fire Hit", 200);
        bonusScoring.addBonuseByName("Holy Hit", 500);
        bonusScoring.addBonuseByName("Holy Hit", 500);
        bonusScoring.addBonuseByName("Holy Hit", 500);
    }
}
