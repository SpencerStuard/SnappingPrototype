using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatDataTypes
{
    public enum CatLimbType
    {
        Body,
        Leg,
        Ear,
        Eye,
        Mouth,
        Tail,
        Butt,
        Horns,
        Wings,
        Nipples
    }

    public enum CatJointType
    {
        Body,

        FrontLeftLeg,
        FrontRightLeg,
        BackLeftLeg,
        BackRightLeg,

        LeftEar,
        RightEar,

        LeftEye,
        RightEye,

        Mouth,
        Tail,
        Butt,
        Horns,
        Wings,
        Nipples
    }


    [System.Serializable]
    public class CatPartReference
    {
        public bool isGoodPart = false;
        public CatFurType furType = null;
        public CatJointType jointType;
        public CatLimbType limbType;
        public CatSet fromSet;

        public CatPartReference(CatJointType jType, CatLimbType lType, CatSet set)
        {
            jointType = jType;
            limbType = lType;
            fromSet = set;
        }
    }


}
