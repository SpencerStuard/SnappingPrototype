using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionControllerBasic : MonoBehaviour {

    public VRSystemInput.Hand hand = VRSystemInput.Hand.Left;

    public List<GameObject> InteractablesInRange = new List<GameObject>();
    public Transform holdPoint;
    public GameObject heldObject;
    public FixedJoint tempHoldJoint;

    VRSystemInput Input;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
        Input = VRSystemInput.Input;
	}

    private int CompareGameObjectDistance(GameObject g1, GameObject g2)
    {
        float d1 = Vector3.Distance(gameObject.transform.position, g1.transform.position);
        float d2 = Vector3.Distance(gameObject.transform.position, g2.transform.position);

        return d1.CompareTo(d2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (!InteractablesInRange.Contains(other.gameObject))
            {
                InteractablesInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (InteractablesInRange.Contains(other.gameObject))
            {
                InteractablesInRange.Remove(other.gameObject);
            }
        }
    }

    private void OnJointBreak(float breakForce)
    {
        heldObject = null;
    }

    void CreateTempJoint()
    {
        tempHoldJoint = gameObject.AddComponent<FixedJoint>();
        tempHoldJoint.breakForce = 10000f;
        tempHoldJoint.breakTorque = 10000f;
    }

    void RemoveTempJoint()
    {
        Object.DestroyImmediate(tempHoldJoint);
    }

    void HoldObject(GameObject go)
    {

        CreateTempJoint();
        heldObject = go;
        go.transform.position = holdPoint.position;
        go.transform.rotation = holdPoint.rotation;

        Rigidbody rb = go.GetComponent<Rigidbody>();
        tempHoldJoint.connectedBody = rb;
        
    }

    void ReleaseObject(GameObject go)
    {

        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity = Input.GetVelocity(hand);
        rb.angularVelocity = Input.GetAngularVelocity(hand);

        heldObject = null;
        RemoveTempJoint();
    }


    // Update is called once per frame
    void Update ()
    {
        if (InteractablesInRange.Count > 0)
        {
            InteractablesInRange.Sort(CompareGameObjectDistance);

            if (Input.GetPickupDown(hand))
            {
                HoldObject(InteractablesInRange[0]);
            }
        }

        if (heldObject != null)
        {

            if (Input.GetPickupUp(hand))
            {
                ReleaseObject(heldObject);
            }
        }

    }
}
