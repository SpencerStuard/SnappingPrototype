using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandActionController : MonoBehaviour {

    public VRSystemInput.Hand hand = VRSystemInput.Hand.Left;

    public List<UseableObject> useablesInRange = new List<UseableObject>();

    VRSystemInput Input;

    // Use this for initialization
    void Start ()
    {
        Input = VRSystemInput.Input;
    }

    public void AddUseableObject(UseableObject o)
    {

    }

    public void RemoveUseableObject(UseableObject o)
    {

    }

    // Update is called once per frame
    void Update ()
    {
	    if(useablesInRange.Count > 0)
        {
            if (Input.GetPickupDown(hand))
            {
                foreach(UseableObject o in useablesInRange)
                {
                    if (o.useable && o.usePickupButton)
                    {
                        o.UseObject(this);
                    }
                }
            }

            else if (Input.GetInteractDown(hand))
            {
                foreach (UseableObject o in useablesInRange)
                {
                    if(o.useable && o.useInteractButton)
                    {
                        o.UseObject(this);
                    }
                }
            }
        }	
	}
}
