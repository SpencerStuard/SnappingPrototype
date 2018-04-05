using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatPartHighlighter : MonoBehaviour
{
    List<Renderer> renderers = new List<Renderer>();
    Dictionary<Renderer, Material> materialDictionary = new Dictionary<Renderer, Material>();
    public Color highlightColor = Color.cyan;
    public float highlightAmount = 0.3f;

    public Material highlightMaterial;



    private void Start()
    {
        BuildMaterialDictionary();
    }

    void BuildMaterialDictionary()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>().ToList();
        foreach(Renderer r in renderers)
        {
            if (materialDictionary.ContainsKey(r)) continue;
            materialDictionary.Add(r, r.material);
        }
    }

    public void ApplyHighlight()
    {
        if (renderers.Count == 0) BuildMaterialDictionary();

        foreach (Renderer r in renderers)
        {
            r.material = highlightMaterial;
        }
    }

    public void RemoveHighlight()
    {
        foreach (Renderer r in renderers)
        {
            r.material = materialDictionary[r];
        }
    }


}
