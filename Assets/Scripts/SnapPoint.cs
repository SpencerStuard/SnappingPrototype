using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public bool occupied;
    public SnappingInteractableObject snappedObject;
    GameObject imposter;
    InteractableObject root;
    FixedJoint snapJoint;
    Rigidbody rb;

    private void Start()
    {
        root = gameObject.GetComponentInParent<InteractableObject>();
    }

    public void SnapObject(SnappingInteractableObject sio, bool isDynamic)
    {
        snappedObject = sio;
        occupied = true;

        //check collision
        CheckForClippedGeo();
        
        //snap position
        sio.transform.position = transform.position;
        sio.transform.rotation = transform.rotation;

        //change object layers
        sio.gameObject.layer = 9;
        foreach (Transform t in sio.transform.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = 9;
        }

        if (isDynamic)
        {
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            snapJoint = gameObject.AddComponent<FixedJoint>();
            snapJoint.connectedBody = sio.rb;
        }
        else
        {
            sio.transform.parent = transform;
            DestroyImmediate(sio.rb);
        }

    }

    void CheckForClippedGeo()
    {
        if (root.beingHeld) return;
        float snappedObjectBounds = snappedObject.combinedBounds.size.magnitude;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, snappedObjectBounds))
        {
            float offset = snappedObjectBounds - hit.distance;
            if (hit.collider.gameObject.layer == 0)
            {
                root.transform.Translate(Vector3.up * offset * 1.1f);
            }

        }
    }

    public void UnsnapObject(SnappingInteractableObject sio, bool isDynamic)
    {

        //change object layers
        sio.gameObject.layer = 8;
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = 8;
        }

        if (isDynamic)
        {
            DestroyImmediate(snapJoint);
            DestroyImmediate(rb);
        }
        else
        {
            sio.rb = sio.gameObject.AddComponent<Rigidbody>();
            sio.transform.parent = sio.presnapParent;
        }

        occupied = false;
        snappedObject = null;
    }
}
