using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJointsForSnapPoints : MonoBehaviour
{

    SnapPoint[] snapPoints;

	// Use this for initialization
	void Start ()
    {
        snapPoints = GetComponentsInChildren<SnapPoint>();

        foreach(SnapPoint p in snapPoints)
        {
            FixedJoint j = gameObject.AddComponent<FixedJoint>();
            j.connectedBody = p.GetComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
