using UnityEngine;
using UnityEditor;
using Esper.ESave;

[CustomEditor(typeof(ScoreManager))]
public class ScoreManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        ScoreManager scoreManager = (ScoreManager)target;
        GameObject currentObject = scoreManager.gameObject;
        if(GUILayout.Button("Clear All Save Data")) {
            SaveFile saveFile = currentObject.GetComponent<SaveFileSetup>().GetSaveFile();
            saveFile.EmptyFile();
        }
    }
}