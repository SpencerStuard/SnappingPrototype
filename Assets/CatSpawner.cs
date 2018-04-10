using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatDataTypes;
using System.Linq;

public class CatSpawner : MonoBehaviour
{
    public CatParts parts;

    public List<CatFurType> furTypes = new List<CatFurType>();
    public List<GameObject> BodyBase = new List<GameObject>();

    //Good parts
    public List<GameObject> FrontLeftLeg = new List<GameObject>();
    public List<GameObject> FrontRightLeg = new List<GameObject>();
    public List<GameObject> BackLeftLeg = new List<GameObject>();
    public List<GameObject> BackRightLeg = new List<GameObject>();
    public List<GameObject> LeftEar = new List<GameObject>();
    public List<GameObject> RightEar = new List<GameObject>();
    public List<GameObject> Mouth = new List<GameObject>();
    public List<GameObject> Tail = new List<GameObject>();
    public List<GameObject> LeftEye = new List<GameObject>();
    public List<GameObject> RightEye = new List<GameObject>();
    public List<GameObject> Butts = new List<GameObject>();

    //Bad parts - Normal Locators
    public List<GameObject> FrontLeftLeg_Bad = new List<GameObject>();
    public List<GameObject> FrontRightLeg_Bad = new List<GameObject>();
    public List<GameObject> BackLeftLeg_Bad = new List<GameObject>();
    public List<GameObject> BackRightLeg_Bad = new List<GameObject>();
    public List<GameObject> LeftEar_Bad = new List<GameObject>();
    public List<GameObject> RightEar_Bad = new List<GameObject>();
    public List<GameObject> Mouth_Bad = new List<GameObject>();
    public List<GameObject> Tail_Bad = new List<GameObject>();
    public List<GameObject> LeftEye_Bad = new List<GameObject>();
    public List<GameObject> RightEye_Bad = new List<GameObject>();
    public List<GameObject> Butts_Bad = new List<GameObject>();

    //Bad Parts - Secondary Locators
    public List<GameObject> Horns_Bad = new List<GameObject>();
    public List<GameObject> Wings_Bad = new List<GameObject>();
    public List<GameObject> Nipples_Bad = new List<GameObject>();

    CatInstance recentlySpawnedCat = null;

    List<CatJointType> PotentialBadJoints = new List<CatJointType>();
    List<CatJointType> GoodJoints = new List<CatJointType>();

    private void Start()
    {
        LoadCatParts();
    }

    void LoadCatParts()
    {
        furTypes = parts.GoodCatFurTypes;

        //Load Good Parts
        CatSet goodSet = parts.GoodCatSets[0];
        BodyBase.Add(goodSet.BodyPrefab);
        FrontLeftLeg.Add(goodSet.FrontLeftLegPrefab);
        FrontRightLeg.Add(goodSet.FrontRightLegPrefab);
        BackLeftLeg.Add(goodSet.BackLeftLegPrefab);
        BackRightLeg.Add(goodSet.BackRightLegPrefab);
        LeftEar.Add(goodSet.LeftEar);
        RightEar.Add(goodSet.RightEar);
        Mouth.Add(goodSet.Mouth);
        Tail.Add(goodSet.Tail);
        LeftEye.Add(goodSet.LeftEye);
        RightEye.Add(goodSet.RightEye);
        Butts.Add(goodSet.Butt);


        //Load Bad Parts
        foreach(CatSet set in parts.BadCatSets)
        {
            if (set.FrontLeftLegPrefab != null) FrontLeftLeg_Bad.Add(set.FrontLeftLegPrefab);
            if (set.FrontRightLegPrefab != null) FrontRightLeg_Bad.Add(set.FrontRightLegPrefab);
            if (set.BackLeftLegPrefab != null) BackLeftLeg_Bad.Add(set.BackLeftLegPrefab);
            if (set.BackRightLegPrefab != null) BackRightLeg_Bad.Add(set.BackRightLegPrefab);
            if (set.LeftEar != null) LeftEar_Bad.Add(set.LeftEar);
            if (set.RightEar != null) RightEar_Bad.Add(set.RightEar);
            if (set.Mouth != null) Mouth_Bad.Add(set.Mouth);
            if (set.Tail != null) Tail_Bad.Add(set.Tail);
            if (set.LeftEye != null) LeftEye_Bad.Add(set.LeftEye);
            if (set.RightEye != null) RightEye_Bad.Add(set.RightEye);
            if (set.Butt != null) Butts_Bad.Add(set.Butt);
            if (set.Horns != null) Horns_Bad.Add(set.Horns);
            if(set.Wings != null) Wings_Bad.Add(set.Horns);
            if(set.Nipples != null) Nipples_Bad.Add(set.Nipples);
        }

        //Generate list of potential bad joints
        if (FrontLeftLeg_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.FrontLeftLeg);
        if (FrontRightLeg_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.FrontRightLeg);
        if (BackLeftLeg_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.BackLeftLeg);
        if (BackRightLeg_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.BackRightLeg);
        if (LeftEar_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.LeftEar);
        if (RightEar_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.RightEar);
        if (Mouth_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.Mouth);
        if (Tail_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.Tail);
        if (LeftEye_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.LeftEye);
        if (RightEye_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.RightEye);
        if (Butts_Bad.Count > 0) PotentialBadJoints.Add(CatJointType.Butt);
        //Add wings, nipples, horns

    }

    public void SpawnCat(int maxNumBadParts = 0)
    {
        if(maxNumBadParts == 0)
        {
            SpawnGoodCat();
        }
        else
        {
            SpawnBadCat(maxNumBadParts);
        }

    }

    bool SpawnBothPartsBad()
    {
        return Random.value > 0.5f;
    }


    void SpawnBadCat(int maxNumBadParts = 0)
    {
        int badPartCount = Random.Range(1, maxNumBadParts);
        badPartCount = Mathf.Clamp(badPartCount, 1, PotentialBadJoints.Count);

        CatFurType furType = furTypes[Random.Range(0, furTypes.Count - 1)];
        GameObject newCat = Instantiate(BodyBase[0], transform.position, transform.rotation);
        CatInstance cat = newCat.GetComponent<CatInstance>();
        recentlySpawnedCat = cat;
        cat.furType = furType;
        furType.ChangePartToMatch(newCat);

        //Pick Our Bad Parts
        List<int> piecesLeftToMake = new List<int>();
        piecesLeftToMake.Add(0);
        piecesLeftToMake.Add(1);
        piecesLeftToMake.Add(2);
        piecesLeftToMake.Add(3);
        piecesLeftToMake.Add(4);
        piecesLeftToMake.Add(5);
        piecesLeftToMake.Add(6);
        piecesLeftToMake.Add(7);
        piecesLeftToMake.Add(8);

        for (int x = 0; x < 9; x++)
        {
            int ranPieceToGet = Random.Range(0, piecesLeftToMake.Count);

            switch (piecesLeftToMake[ranPieceToGet])
            {
                case 0:
                    if (badPartCount > cat.badPartsAssigned && FrontLeftLeg_Bad.Count > 0)
                    {
                        SpawnBadFrontLegs(cat);
                    }
                    else
                    {
                        SpawnGoodFrontLegs(cat);
                    }
                    break;

                case 1:
                    if (badPartCount > cat.badPartsAssigned && BackLeftLeg_Bad.Count > 0)
                    {
                        SpawnBadBackLegs(cat);
                    }
                    else
                    {
                        SpawnGoodBackLegs(cat);
                    }
                    break;

                case 2:
                    if (badPartCount > cat.badPartsAssigned && LeftEye_Bad.Count > 0)
                    {
                        SpawnBadEyes(cat);
                    }
                    else
                    {
                        SpawnGoodEyes(cat);
                    }
                    break;

                case 3:
                    if (badPartCount > cat.badPartsAssigned && LeftEar_Bad.Count > 0)
                    {
                        SpawnBadEars(cat);
                    }
                    else
                    {
                        SpawnGoodEars(cat);
                    }
                    break;

                case 4:
                    if (badPartCount > cat.badPartsAssigned && Tail_Bad.Count > 0)
                    {
                        SpawnBadTail(cat);
                    }
                    else
                    {
                        SpawnGoodTail(cat);
                    }
                    break;

                case 5:
                    if (badPartCount > cat.badPartsAssigned && Mouth_Bad.Count > 0)
                    {
                        SpawnBadMouth(cat);
                    }
                    else
                    {
                        SpawnGoodMouth(cat);
                    }
                    break;

                case 6:
                    if (badPartCount > cat.badPartsAssigned && Wings_Bad.Count > 0)
                    {
                        SpawnBadWings(cat);
                    }
                    else
                    {
                        //Do Nothing
                    }
                    break;

                case 7:
                    if (badPartCount > cat.badPartsAssigned && Horns_Bad.Count > 0)
                    {
                        SpawnBadHorns(cat);
                    }
                    else
                    {
                        //DoNothing
                    }
                    break;

                case 8:
                    if (badPartCount > cat.badPartsAssigned && Butts_Bad.Count > 0)
                    {
                        SpawnBadButt(cat);
                    }
                    else
                    {
                        SpawnGoodButt(cat);
                    }
                    break;
                default:
                    Debug.Log("Error Getting the right piece number");
                    break;

            }
            piecesLeftToMake.Remove(piecesLeftToMake[ranPieceToGet]);
        }



    }

    void SpawnGoodCat()
    {
        CatFurType furType = furTypes[Random.Range(0, furTypes.Count - 1)];
        GameObject newCat = Instantiate(BodyBase[0], transform.position, transform.rotation);
        CatInstance cat = newCat.GetComponent<CatInstance>();
        cat.furType = furType;
        furType.ChangePartToMatch(newCat);


        cat.FrontLeftLegSnap.SpawnAndSnapFromPrefab(FrontLeftLeg[0], cat);
        cat.FrontRightLegSnap.SpawnAndSnapFromPrefab(FrontRightLeg[0], cat);
        cat.BackLeftLegSnap.SpawnAndSnapFromPrefab(BackLeftLeg[0], cat);
        cat.BackRightLegSnap.SpawnAndSnapFromPrefab(BackRightLeg[0], cat);
        cat.LeftEarSnap.SpawnAndSnapFromPrefab(LeftEar[0], cat);
        cat.RightEarSnap.SpawnAndSnapFromPrefab(RightEar[0], cat);
        cat.MouthSnap.SpawnAndSnapFromPrefab(Mouth[0], cat);
        cat.TailSnap.SpawnAndSnapFromPrefab(Tail[0], cat);
        cat.LeftEyeSnap.SpawnAndSnapFromPrefab(LeftEye[0], cat);
        cat.RightEyeSnap.SpawnAndSnapFromPrefab(RightEye[0], cat);
        cat.ButtSnap.SpawnAndSnapFromPrefab(Butts[0], cat);
    }

    void SpawnGoodFrontLegs(CatInstance cat)
    {
        cat.FrontLeftLegSnap.SpawnAndSnapFromPrefab(FrontLeftLeg[0], cat);
        cat.FrontRightLegSnap.SpawnAndSnapFromPrefab(FrontRightLeg[0], cat);
    }

    void SpawnGoodBackLegs(CatInstance cat)
    {
        cat.BackLeftLegSnap.SpawnAndSnapFromPrefab(BackLeftLeg[0], cat);
        cat.BackRightLegSnap.SpawnAndSnapFromPrefab(BackRightLeg[0], cat);
    }

    void SpawnGoodEars(CatInstance cat)
    {
        cat.LeftEarSnap.SpawnAndSnapFromPrefab(LeftEar[0], cat);
        cat.RightEarSnap.SpawnAndSnapFromPrefab(RightEar[0], cat);
    }

    void SpawnGoodEyes(CatInstance cat)
    {
        cat.LeftEyeSnap.SpawnAndSnapFromPrefab(LeftEye[0], cat);
        cat.RightEyeSnap.SpawnAndSnapFromPrefab(RightEye[0], cat);
    }

    void SpawnGoodMouth(CatInstance cat)
    {
        cat.MouthSnap.SpawnAndSnapFromPrefab(Mouth[0], cat);
    }

    void SpawnGoodTail(CatInstance cat)
    {
        cat.TailSnap.SpawnAndSnapFromPrefab(Tail[0], cat);
    }

    void SpawnGoodButt(CatInstance cat)
    {
        cat.ButtSnap.SpawnAndSnapFromPrefab(Butts[0], cat);
    }


    void SpawnBadFrontLegs(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, FrontLeftLeg_Bad.Count - 1);

        if (SpawnBothPartsBad())
        {
            cat.FrontLeftLegSnap.SpawnAndSnapFromPrefab(FrontLeftLeg_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;

            cat.FrontRightLegSnap.SpawnAndSnapFromPrefab(FrontRightLeg_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;
        }
        else
        {
            if(Random.value > 0.5f)
            {
                //good left
                cat.FrontLeftLegSnap.SpawnAndSnapFromPrefab(FrontLeftLeg[0], cat);

                //bad right
                cat.FrontRightLegSnap.SpawnAndSnapFromPrefab(FrontRightLeg_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
            else
            {
                //good right
                cat.FrontRightLegSnap.SpawnAndSnapFromPrefab(FrontRightLeg[0], cat);

                //bad left
                cat.FrontLeftLegSnap.SpawnAndSnapFromPrefab(FrontLeftLeg_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
        }
    }

    void SpawnBadBackLegs(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, BackLeftLeg_Bad.Count - 1);

        if (SpawnBothPartsBad())
        {
            cat.BackLeftLegSnap.SpawnAndSnapFromPrefab(BackLeftLeg_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;

            cat.BackRightLegSnap.SpawnAndSnapFromPrefab(BackRightLeg_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;
        }
        else
        {
            if (Random.value > 0.5f)
            {
                //good left
                cat.BackLeftLegSnap.SpawnAndSnapFromPrefab(BackRightLeg[0], cat);

                //bad right
                cat.BackRightLegSnap.SpawnAndSnapFromPrefab(BackRightLeg_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
            else
            {
                //good right
                cat.BackRightLegSnap.SpawnAndSnapFromPrefab(BackRightLeg[0], cat);

                //bad left
                cat.BackLeftLegSnap.SpawnAndSnapFromPrefab(BackLeftLeg_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
        }
    }

    void SpawnBadEars(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, LeftEar_Bad.Count - 1);

        if (SpawnBothPartsBad())
        {
            cat.LeftEarSnap.SpawnAndSnapFromPrefab(LeftEar_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;

            cat.RightEarSnap.SpawnAndSnapFromPrefab(RightEar_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;
        }
        else
        {
            if (Random.value > 0.5f)
            {
                //good left
                cat.LeftEarSnap.SpawnAndSnapFromPrefab(LeftEar[0], cat);

                //bad right
                cat.RightEarSnap.SpawnAndSnapFromPrefab(RightEar_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
            else
            {
                //good right
                cat.RightEarSnap.SpawnAndSnapFromPrefab(RightEar[0], cat);

                //bad left
                cat.LeftEarSnap.SpawnAndSnapFromPrefab(LeftEar_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
        }
    }

    void SpawnBadEyes(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, LeftEye_Bad.Count - 1);

        if (SpawnBothPartsBad())
        {
            cat.LeftEyeSnap.SpawnAndSnapFromPrefab(LeftEye_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;

            cat.RightEyeSnap.SpawnAndSnapFromPrefab(RightEye_Bad[badPartIndex], cat);
            cat.badPartsAssigned++;
        }
        else
        {
            if (Random.value > 0.5f)
            {
                //good left
                cat.LeftEyeSnap.SpawnAndSnapFromPrefab(LeftEye[0], cat);

                //bad right
                cat.RightEyeSnap.SpawnAndSnapFromPrefab(RightEye_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
            else
            {
                //good right
                cat.RightEyeSnap.SpawnAndSnapFromPrefab(RightEye[0], cat);

                //bad left
                cat.LeftEyeSnap.SpawnAndSnapFromPrefab(LeftEye_Bad[badPartIndex], cat);
                cat.badPartsAssigned++;
            }
        }
    }

    void SpawnBadMouth(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, Mouth_Bad.Count - 1);
        cat.MouthSnap.SpawnAndSnapFromPrefab(Mouth_Bad[badPartIndex], cat);
    }

    void SpawnBadTail(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, Tail_Bad.Count - 1);
        cat.TailSnap.SpawnAndSnapFromPrefab(Tail_Bad[badPartIndex], cat);
    }

    void SpawnBadButt(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, Butts_Bad.Count - 1);
        cat.ButtSnap.SpawnAndSnapFromPrefab(Butts_Bad[badPartIndex], cat);
    }

    void SpawnBadWings(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, Wings_Bad.Count - 1);
        cat.WingsSnap.SpawnAndSnapFromPrefab(Wings_Bad[badPartIndex], cat);
    }

    void SpawnBadHorns(CatInstance cat)
    {
        int badPartIndex = Random.Range(0, Horns_Bad.Count - 1);
        cat.HornsSnap.SpawnAndSnapFromPrefab(Horns_Bad[badPartIndex], cat);
    }

    public GameObject GetRandomPrefabFromList(List<GameObject> prefabList)
    {
        int index = Random.Range(0, prefabList.Count - 1);
        return prefabList[index];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCat();
        }
    }

}
