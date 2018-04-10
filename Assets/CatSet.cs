using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatDataTypes;

[System.Serializable, CreateAssetMenu(menuName = "CatSet", fileName = "CatSet_Default", order = 0)]
public class CatSet : ScriptableObject
{
    public string Name;
    public bool unlocked = false;
    public bool isGoodSet = false;

    public Texture bodyTexture; //Irrelevent for Good Set
    public Texture eyeTexture;

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
    

    public void AssignBodyTextureToGameObject(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material.mainTexture = bodyTexture;
        }
    }


    public void AssignEyeTextureToGameObject(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material.mainTexture = eyeTexture;
        }
    }


    public GameObject GetPrefabForJointType(CatJointType jointType)
    {
        switch (jointType)
        {
            case CatJointType.FrontLeftLeg:
                return FrontLeftLegPrefab;
            case CatJointType.FrontRightLeg:
                return FrontRightLegPrefab;
            case CatJointType.BackLeftLeg:
                return BackLeftLegPrefab;
            case CatJointType.BackRightLeg:
                return BackRightLegPrefab;
            case CatJointType.LeftEar:
                return LeftEar;
            case CatJointType.RightEar:
                return RightEar;
            case CatJointType.LeftEye:
                return LeftEye;
            case CatJointType.RightEye:
                return RightEye;
        }

        return null;

    }
}
