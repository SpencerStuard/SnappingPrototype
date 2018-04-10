using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatDataTypes;

public class CatSnapPoint : MonoBehaviour
{

    public CatJointType jointType;
    public CatLimbType limbType;
    public bool occupied;
    public SnappingCatPart snappedObject;

    bool spawnedWithBadPart = false;
    bool addedPoints = false;
    public bool satisfied = true;

    InteractableObject root;
    CatInstance cat;
    FixedJoint snapJoint;
    Rigidbody rb;

    private void Start()
    {
        root = gameObject.GetComponentInParent<InteractableObject>();
        if(cat == null) cat = gameObject.GetComponentInParent<CatInstance>();
    }

    void SnapObject(SnappingCatPart sio)
    {
        bool isDynamic = sio.isDynamic;
        snappedObject = sio;
        occupied = true;


        //check collision
        if(root != null) CheckForClippedGeo();
        
        //snap position
        sio.transform.position = transform.position;
        if(sio.partReference.limbType == limbType) sio.transform.rotation = transform.rotation;

        //chatnge object layers
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
            DestroyImmediate(sio.gameObject.GetComponent<Rigidbody>());
        }

    }

    public void ValidatePartAndPreview()
    {

    }

    public void ValidatePartAndSnap(SnappingCatPart sio)
    {
        bool goodSnap = false;

        if(limbType == sio.partReference.limbType)
        {
            //placed into right limb type
            goodSnap = sio.partReference.isGoodPart;

            if (jointType != sio.partReference.jointType)
            {
                sio = SwapForCorrectPart(sio);
            }
        }

        if (sio.partReference.isGoodPart && cat.furType != sio.partReference.furType)
        {
            cat.furType.ChangePartToMatch(sio.gameObject);
        }

        //Handle Scoring
        if (goodSnap)
        {
            if(spawnedWithBadPart && !addedPoints)
            {
                cat.CatPointValue += sio.pointValue;
                addedPoints = true;
            }

            satisfied = true;
        }


        SnapObject(sio);
    }

    SnappingCatPart SwapForCorrectPart(SnappingCatPart sio)
    {
        SnappingCatPart newPart = SpawnPartFromSet(sio.partReference.fromSet);
        DestroyImmediate(sio.gameObject);
        return newPart;
    }

    public SnappingCatPart SpawnPartFromSet(CatSet set)
    {
        GameObject prefabToSpawn = set.GetPrefabForJointType(jointType);
        GameObject newPart = (GameObject)Instantiate(prefabToSpawn, transform.position, transform.rotation);
        SnappingCatPart newCatPart = newPart.GetComponent<SnappingCatPart>();
        newCatPart.snapped = true;
        newCatPart.snappedToPoint = this;
        newCatPart.presnapParent = null;
        return newCatPart;
    }


    public void SpawnAndSnapFromPrefab(GameObject prefab, CatInstance c)
    {
        cat = c;
        GameObject newPart = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
        SnappingCatPart newCatPart = newPart.GetComponent<SnappingCatPart>();
        newCatPart.snapped = true;
        newCatPart.snappedToPoint = this;
        newCatPart.presnapParent = null;
        cat.furType.ChangePartToMatch(newPart);

        if (newCatPart.partReference.isGoodPart)
        {
            //fix fur
            if(cat.furType != newCatPart.partReference.furType)
            {
                cat.furType.ChangePartToMatch(newPart);
            }

            satisfied = true;
            spawnedWithBadPart = false;
        }
        else
        {
            satisfied = false;
            spawnedWithBadPart = true;

            if(limbType == CatLimbType.Eye)
            {
                newCatPart.partReference.fromSet.AssignEyeTextureToGameObject(newPart);
            }
            else
            {
                newCatPart.partReference.fromSet.AssignBodyTextureToGameObject(newPart);

            }
        }

        SnapObject(newCatPart);
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

    public virtual void UnsnapObject(SnappingCatPart sio)
    {
        bool isDynamic = sio.isDynamic;

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
        satisfied = false;
    }
}
