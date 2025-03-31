using UnityEngine;

[CreateAssetMenu(fileName = "ConvertTile", menuName = "Scriptable Objects/Oracle Actions/OracleAction1")]
public class ConvertTile : OraclePack
{

    [SerializeField]
    private ScoringAreaConfigSO[] scoringAreaConfigSOs;
    public override void PerformAction()
    {
        GameObject gameBoard = GameObject.Find("Board Generator");
        var scoringAreaConfig = scoringAreaConfigSOs[UnityEngine.Random.Range(0, scoringAreaConfigSOs.Length)].getScoringAreaConfig();
        var scoringAreas = gameBoard.GetComponent<HexMountainGenerator>().scoringAreaGameObject;
        GameObject randomScoringArea = scoringAreas[UnityEngine.Random.Range(0, scoringAreas.Count)];
        while (randomScoringArea.GetComponent<ScoringTriggerZone>().getElementType() == scoringAreaConfig.elementType)
        {
            randomScoringArea = scoringAreas[UnityEngine.Random.Range(0, scoringAreas.Count)];
        }
        randomScoringArea.GetComponent<ScoringTriggerZone>().UpdateSetting(scoringAreaConfig);
    }

    public override string Description => "Converts a scoring tile to a random different element.";
}
