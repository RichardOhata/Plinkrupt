using System;
using UnityEngine;


[RequireComponent(typeof(ScoringTriggerZone))]
public class ScoringAreaVisualModifier : MonoBehaviour
{
    private ScoringTriggerZone _scoringTriggerZone => GetComponent<ScoringTriggerZone>();
    private MeshRenderer _meshRenderer => gameObject.GetComponent<MeshRenderer>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        _scoringTriggerZone.OnScoreAreaVisualChangeEvent += onVisualChange;
    }

    private void onVisualChange(Color color)
    {
        _meshRenderer.material.SetColor("_BaseColor", color);
    }


}
