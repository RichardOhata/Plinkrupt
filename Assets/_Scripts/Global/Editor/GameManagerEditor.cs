using UnityEngine;

using UnityEditor;
using Esper.ESave;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        GameManager gameManager = (GameManager)target;
        GameObject currentObject = gameManager.gameObject;
        if(GUILayout.Button("Clear All Save Data")) {
            SaveFile saveFile = currentObject.GetComponent<SaveFileSetup>().GetSaveFile();
            saveFile.EmptyFile();
        }
    }
}