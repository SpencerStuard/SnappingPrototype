using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CatParts", fileName = "CatParts_Default", order = 0)]
public class CatParts : ScriptableObject
{
    [System.Serializable]
    public class CatPartPrefab
    {
        public GameObject partPrefab;
        public CatSet fromSet;
    }

    //Good parts
    public List<CatSet> GoodCatSets = new List<CatSet>();

    /*
    public List<CatPartPrefab> GoodBodyBase = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodFrontLeftLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodFrontRightLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodBackLeftLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodBackRightLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodLeftEar = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodRightEar = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodMouth = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodTail = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodLeftEye = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodRightEye = new List<CatPartPrefab>();
    public List<CatPartPrefab> GoodButts = new List<CatPartPrefab>();
    */


    //Bad parts - Normal Locators
    public List<CatPartPrefab> BadBodyBase = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadFrontLeftLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadFrontRightLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadBackLeftLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadBackRightLeg = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadLeftEar = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadRightEar = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadMouth = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadTail = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadLeftEye = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadRightEye = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadButts = new List<CatPartPrefab>();

    //Bad Parts - Secondary Locators
    public List<CatPartPrefab> BadHorns = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadWings = new List<CatPartPrefab>();
    public List<CatPartPrefab> BadNipples = new List<CatPartPrefab>();

    public static GameObject GetGoodPart(CatDataTypes.CatJointType jType, int index = -1)
    {
        return null;
    }

    public static GameObject GetBadPart(CatDataTypes.CatJointType jType, int index = -1)
    {
        return null;
    }

    public static GameObject GetPartFromSet(CatDataTypes.CatJointType jType, CatSet set)
    {
        return null;
    }
}
