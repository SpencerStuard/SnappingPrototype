using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CatDataTypes
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
    public class CatPartType
    {
        public CatJointType jointType;
        public CatLimbType limbType;
    }

    [System.Serializable]
    public class CatPartInstance
    {
        public CatPartType partType;
        public CatSet fromSet;
    }
}
