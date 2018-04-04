using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CatParts", fileName = "CatParts_Default", order = 0)]
public class CatParts : ScriptableObject
{
    public List<CatFurType> GoodCatFurTypes = new List<CatFurType>();
    public List<CatSet> GoodCatSets = new List<CatSet>();
    public List<CatSet> BadCatSets = new List<CatSet>();
    


    
    
}
