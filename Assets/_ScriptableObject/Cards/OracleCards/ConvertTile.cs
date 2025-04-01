using UnityEngine;

[CreateAssetMenu(fileName = "ConvertTile", menuName = "Scriptable Objects/Oracle Actions/OracleAction1")]
public class ConvertTile : OraclePack
{
    [SerializeField]
    private ScoringAreaConfigSO targetScoringConfig;
    public override void PerformAction()
    {
        GameObject gameBoard = GameObject.Find("Board Generator");
        if (gameBoard == null)
        {
            Debug.LogError("GameBoard not found!");
            return;
        }

        var scoringAreaConfig = targetScoringConfig.getScoringAreaConfig();
        var scoringAreas = gameBoard.GetComponent<HexMountainGenerator>().scoringAreaGameObject;

        // Find all areas that aren't already this element type
        var validAreas = new System.Collections.Generic.List<GameObject>();
        foreach (var area in scoringAreas)
        {
            if (area.GetComponent<ScoringTriggerZone>().getElementType() != scoringAreaConfig.elementType)
            {
                validAreas.Add(area);
            }
        }

        if (validAreas.Count == 0)
        {
            Debug.LogWarning("No valid areas to convert!");
            return;
        }

        // Convert a random valid area
        GameObject randomScoringArea = validAreas[UnityEngine.Random.Range(0, validAreas.Count)];
        randomScoringArea.GetComponent<ScoringTriggerZone>().UpdateSetting(scoringAreaConfig);
    
}

    public override string Description => $"Converts a scoring tile to <color=#{ColorUtility.ToHtmlStringRGB(targetScoringConfig.getColor())}>" +
        $"{targetScoringConfig.elementType}</color> element";
}
