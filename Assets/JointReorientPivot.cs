using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class JointReorientPivot : MonoBehaviour {

    public Transform newPivotTransform;


    [ContextMenu("Generate Objects for New Pivot")]
    public void CreateNewSnapPointAndJoint()
    {
        string snapPointName = transform.name;
        string prefabName = transform.GetChild(0).name;

        Transform prefabTransform = transform.GetChild(0);

        var SnapPointComponents = GetComponents<Component>();
        var PrefabComponents = prefabTransform.GetComponents<Component>();

        //Create Destination Prefab object
        GameObject newPrefabObject = new GameObject(prefabName);
        newPrefabObject.transform.position = newPivotTransform.position;
        newPrefabObject.transform.rotation = newPivotTransform.rotation;
        newPrefabObject.transform.parent = newPivotTransform;

        //Copy snap point stuff to new snap point
        foreach(Component c in SnapPointComponents)
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(c);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newPivotTransform.gameObject);
        }

        //Copy snap point stuff to new prefab object
        foreach (Component c in PrefabComponents)
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(c);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newPrefabObject);
        }

        //move prefab children to new prefab object
        List<Transform> children = new List<Transform>();
        for(int i = 0; i < prefabTransform.childCount; i++)
        {
            children.Add(prefabTransform.GetChild(i));
        }

        foreach(Transform t in children)
        {
            t.parent = newPrefabObject.transform;
        }


        newPivotTransform.name = snapPointName;

        DestroyImmediate(gameObject);
    }
}
