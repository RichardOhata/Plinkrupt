using UnityEngine;
using UnityEditor;
using Esper.ESave;

[CustomEditor(typeof(ElementMultiplierManager))]
public class ElementMultiplierManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        ElementMultiplierManager EmManager = (ElementMultiplierManager)target;
        GameObject currentObject = EmManager.gameObject;
        if(GUILayout.Button("Clear All Save Data")) {
            SaveFile saveFile = currentObject.GetComponent<SaveFileSetup>().GetSaveFile();
            saveFile.EmptyFile();
        }
    }
}