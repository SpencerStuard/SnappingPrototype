using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInstance : MonoBehaviour
{
    public CatSet fromSet;
    public CatFurType furType;

    public CatSnapPoint FrontLeftLegSnap;
    public CatSnapPoint FrontRightLegSnap;
    public CatSnapPoint BackLeftLegSnap;
    public CatSnapPoint BackRightLegSnap;
    public CatSnapPoint LeftEarSnap;
    public CatSnapPoint RightEarSnap;
    public CatSnapPoint MouthSnap;
    public CatSnapPoint TailSnap;
    public CatSnapPoint LeftEyeSnap;
    public CatSnapPoint RightEyeSnap;
    public CatSnapPoint ButtSnap;
    public CatSnapPoint HornsSnap;
    public CatSnapPoint WingsSnap;
    public CatSnapPoint NipplesSnap;

    List<CatSnapPoint> requiredSnapPoints = new List<CatSnapPoint>();

    public int badPartsAssigned = 0;
    public int CatPointValue = 100;

    public bool IsCatComplete()
    {
        bool isGood = true;
        foreach(CatSnapPoint p in requiredSnapPoints)
        {
            if (!p.satisfied) isGood = false;
        }

        return isGood;
    }

    private void Awake()
    {
        requiredSnapPoints.Add(FrontLeftLegSnap);
        requiredSnapPoints.Add(FrontRightLegSnap);
        requiredSnapPoints.Add(BackLeftLegSnap);
        requiredSnapPoints.Add(BackRightLegSnap);
        requiredSnapPoints.Add(LeftEarSnap);
        requiredSnapPoints.Add(RightEarSnap);
        requiredSnapPoints.Add(MouthSnap);
        requiredSnapPoints.Add(TailSnap);
        requiredSnapPoints.Add(LeftEyeSnap);
        requiredSnapPoints.Add(RightEyeSnap);
        requiredSnapPoints.Add(ButtSnap);
    }

}
