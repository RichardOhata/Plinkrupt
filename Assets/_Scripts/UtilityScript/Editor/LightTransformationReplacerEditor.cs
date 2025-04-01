using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightTransformationReplacer))]
public class LightTransformationReplacerEditor : Editor {
    public override void OnInspectorGUI() {

        DrawDefaultInspector(); 

        LightTransformationReplacer group = (LightTransformationReplacer)target;

        if(GUILayout.Button("1. Replace Transformation preview")){
            if(!group.fromObjectParentGroup || !group.toObjectParentGroup){
                Debug.LogError("fromObjectParentGroup or toObjectParentGroup is not set.");
            }

            ReplaceTarget[] fromObject = group.fromObjectParentGroup.GetComponentsInChildren<ReplaceTarget>();
            ReplaceTarget[] toObject = group.toObjectParentGroup.GetComponentsInChildren<ReplaceTarget>();
            GameObject createNewObject = group.createNewObject.GetComponent<GameObject>();

            group.fromObject.Clear();
            group.toObject.Clear();

            foreach(var from in fromObject){
                Transform currentTransform = from.transform;
                group.fromObject.Add(currentTransform);
            }

            foreach(var to in toObject){
                Transform currentTransform = to.transform;
                group.toObject.Add(currentTransform);
            }

            EditorUtility.SetDirty(group);
        }
        else if(GUILayout.Button("2. Start Transformation")){
            for(int i=0; i<group.fromObject.Count; i++){
                group.toObject[i].position = group.fromObject[i].position;
                group.toObject[i].rotation = group.fromObject[i].rotation;
                group.toObject[i].localScale = group.fromObject[i].localScale;
            }

            group.fromObject.Clear();
            group.toObject.Clear();
            EditorUtility.SetDirty(group);
        }
        else if(GUILayout.Button("2a: Create new Instance")){
            for(int i=0; i<group.fromObject.Count; i++){
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(group.createNewObject);
                newObject.transform.position = group.fromObject[i].position;
                newObject.transform.rotation = group.fromObject[i].rotation;
                newObject.transform.localScale = group.fromObject[i].localScale;

                newObject.transform.SetParent(group.toObjectParentGroup.transform);
                newObject.name = group.createNewObject.name + "_"  + i;
            }
            
        }
    }
}

