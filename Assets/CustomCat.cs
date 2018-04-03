using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Cat", fileName = "CustomCat_MyCat", order = 0)]
public class CustomCat : ScriptableObject
{
    [System.Serializable]
    public class CatComponentData
    {
        public int index = -1;
        public bool isGoodPart = true;

        public CatComponentData(int i, bool g)
        {
            index = i;
            isGoodPart = g;
        }
    }


    public CatComponentData Body;
    public CatComponentData FrontLeftLeg;
    public CatComponentData FrontRightLeg;
    public CatComponentData BackLeftLeg;
    public CatComponentData BackRightLeg;
    public CatComponentData LeftEar;
    public CatComponentData RightEar;
    public CatComponentData Mouth;
    public CatComponentData Tail;
    public CatComponentData LeftEye;
    public CatComponentData RightEye;
    public CatComponentData Butt;
    public CatComponentData Horns;
    public CatComponentData Wings;
    public CatComponentData Nipples;

}
