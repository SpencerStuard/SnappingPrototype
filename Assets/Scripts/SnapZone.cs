using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnapZone : MonoBehaviour
{

    public List<SnapPoint> snapPoints = new List<SnapPoint>();
    public Highlighter highlighter;

	// Use this for initialization
	void Start ()
    {
        snapPoints = GetComponentsInChildren<SnapPoint>().ToList();	
	}
	

    public SnapPoint BestSnapPointForSnappingObject(SnappingInteractableObject snapObject)
    {
        float bestDist = Mathf.Infinity;
        SnapPoint bestPoint = null;

        foreach(SnapPoint p in snapPoints)
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
