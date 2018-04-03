using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CatSnapZone : MonoBehaviour
{

    public List<CatSnapPoint> snapPoints = new List<CatSnapPoint>();
    public Highlighter highlighter;

	// Use this for initialization
	void Start ()
    {
        snapPoints = GetComponentsInChildren<CatSnapPoint>().ToList();	
	}
	

    public CatSnapPoint BestSnapPointForSnappingObject(SnappingCatPart snapObject)
    {
        float bestDist = Mathf.Infinity;
        CatSnapPoint bestPoint = null;

        foreach(CatSnapPoint p in snapPoints)
        {
            if (!p.occupied)
            {
                float dist = Vector3.Distance(snapObject.transform.position, p.transform.position);
                if(dist < bestDist)
                {
                    bestDist = dist;
                    bestPoint = p;
                }
            }
        }

        return bestPoint;

    }



	// Update is called once per frame
	void Update ()
    {
	    	
	}
}
