using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScoreAnimation))]
public class ScoreAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScoreAnimation current = (ScoreAnimation)target;

        if(GUILayout.Button("Test Update Score")){
            current.UpdateScore(1000);
        }
       
    }
}