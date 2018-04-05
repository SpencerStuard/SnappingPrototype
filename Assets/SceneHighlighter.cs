using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHighlighter : MonoBehaviour
{
    public static SceneHighlighter instance;

    public Color HighlightColor = Color.white;
    public float HighlightAmount = 0.875f;

    private void Awake()
    {
        instance = this;   
    }

    public void ApplyHighlightToGameObject(GameObject g)
    {
        foreach (MeshRenderer r in g.GetComponentsInChildren<MeshRenderer>())
        {
            for (int i = 0; i < r.materials.Length; i++)
            {
                r.materials[i].SetColor("_Color", HighlightColor);
                r.materials[i].SetFloat("_Highlight", HighlightAmount);
            }
        }
    }

    public void RemoveHighlightFromGameObject(GameObject g)
    {
        foreach (MeshRenderer r in g.GetComponentsInChildren<MeshRenderer>())
        {
            for (int i = 0; i < r.materials.Length; i++)
            {
                r.materials[i].SetFloat("_Highlight", 0);
            }
        }
    }
}
