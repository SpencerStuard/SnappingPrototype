using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatDataTypes;

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
    }

    public void SpawnCat(int numBadParts = 0)
    {
        if(numBadParts == 0)
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
