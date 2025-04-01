using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CustomEditor(typeof(ScoringTriggerZone))]
public class ScoringTriggerZoneEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        ScoringTriggerZone current = (ScoringTriggerZone)target;

        if(GUILayout.Button("Add Element Visual Modifier")){
            current.AddComponent<ScoringAreaVisualModifier>();
            EditorUtility.SetDirty(current);
        }
    }
}

