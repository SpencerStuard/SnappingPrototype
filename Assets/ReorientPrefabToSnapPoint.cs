using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorientPrefabToSnapPoint : MonoBehaviour {

    public Transform snapPoint;


    [ContextMenu("Reorient Part To Snap Point")]
    public void ReorientPart()
    {
        GameObject newPrefabObject = new GameObject(transform.name);
        newPrefabObject.transform.position = snapPoint.position;
        newPrefabObject.transform.rotation = snapPoint.rotation;
        newPrefabObject.transform.parent = snapPoint;

        var PrefabComponents = GetComponents<Component>();
        foreach (Component c in PrefabComponents)
        {
            UnityEditorInternal.ComponentUtility.CopyComponent(c);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newPrefabObject);
        }

        List<Transform> children = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }

        foreach (Transform t in children)
        {
            t.parent = newPrefabObject.transform;
        }

        DestroyImmediate(gameObject);
    }
}
