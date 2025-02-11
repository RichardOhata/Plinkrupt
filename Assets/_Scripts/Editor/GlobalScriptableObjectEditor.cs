using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ScriptableObject), true )]
public class GlobalScriptableObjectEditor : Editor{

    
    /// <summary>
    /// Custom editor for scriptable objects to save them without needing to click on the "Assets" menu.
    /// </summary>
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        if (GUILayout.Button("Save")){
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Asset {target.name} has been Saved ");
        }
    }
}
