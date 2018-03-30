using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HighlightStyle", fileName = "HighlightStyle", order = 0)]
public class Highlighter : ScriptableObject
{
    public Color highlightColor = Color.cyan;
    public Material highlightMaterial;


    public void ApplyHighlightToGameObject(GameObject g)
    {
        foreach(MeshRenderer r in g.GetComponentsInChildren<MeshRenderer>())
        {
            r.sharedMaterial = highlightMaterial;
            for(int i = 0; i < r.materials.Length; i ++)
            {
                r.materials[i] = highlightMaterial;
                r.materials[i].color = highlightColor;
            }
        }
    }

}
