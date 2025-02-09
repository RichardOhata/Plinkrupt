using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoringTriggerZone : MonoBehaviour, IElementScoreInteraction
{

    public ScoringAreaConfigSO scoringAreaConfigSo;
    private ScoringClass.ElementType ObjectElement;
    private float BaseMultiplier;
    
    void Start()
    {
        if(!scoringAreaConfigSo){
            Debug.LogWarning("Scoring Trigger So is not set!");
            return;
        }
        ObjectElement = scoringAreaConfigSo.elementType;
        BaseMultiplier = scoringAreaConfigSo.baseScoreMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision){
        Debug.Log("Triggered");
        if(collision.gameObject.TryGetComponent<IElementScoreInteraction>(out IElementScoreInteraction elementScoreInteraction)){
            float multiplier = GameManager.instance.elementMultiplierTable.GetMultiplier(ObjectElement, elementScoreInteraction.getElementType());
            Debug.Log($"Score Area: {ObjectElement}, Target Element: {elementScoreInteraction.getElementType()}, Multiplier: {multiplier}");
        }
    }

    public float getBaseMutiplier()
    {
        return BaseMultiplier;
    }

    public ScoringClass.ElementType getElementType()
    {
        return ObjectElement;
    }
}
