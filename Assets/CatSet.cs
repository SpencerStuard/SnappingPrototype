using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "CatSet", fileName = "CatSet_Default", order = 0)]
public class CatSet : ScriptableObject
{
    public string Name;
    public bool isGoodSet = false;
    public bool unlocked = false;

    public List<CatFurType> validFurTypes = new List<CatFurType>();
    public List<CatEyeType> validEyeTypes = new List<CatEyeType>();

    public GameObject BodyPrefab;
    public GameObject FrontLeftLegPrefab;
    public GameObject FrontRightLegPrefab;
    public GameObject BackLeftLegPrefab;
    public GameObject BackRightLegPrefab;
    public GameObject LeftEar;
    public GameObject RightEar;
    public GameObject LeftEye;
    public GameObject RightEye;
    public GameObject Mouth;
    public GameObject Tail;
    public GameObject Butt;
    public GameObject Horns;
    public GameObject Wings;
    public GameObject Nipples;
    
}
