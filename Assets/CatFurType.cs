using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CatFurType", fileName = "CatFurType_Default", order = 0)]
public class CatFurType : ScriptableObject
{
    public Texture furTexture;

    public void ChangePartToMatch(GameObject g)
    {
        Renderer[] renderers = g.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.material.mainTexture = furTexture;
        }
    }
}
