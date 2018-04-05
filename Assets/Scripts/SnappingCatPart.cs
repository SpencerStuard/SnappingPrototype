using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CatDataTypes;

public class SnappingCatPart : InteractableObject
{
    public bool snapped = false;
    public CatSnapPoint snappedToPoint;
    public Transform presnapParent = null;
    public bool inSnapZone = false;
    public List<CatSnapZone> snapZones = new List<CatSnapZone>();
    public CatSnapPoint potentialSnapPoint;
    public GameObject previewSnapObject;
    [HideInInspector]public Bounds combinedBounds;
    public bool isDynamic = false;

    //CatStuff
    public CatPartReference partReference;
    public CatPartHighlighter highlighter;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        CalculateCombinedBounds();
        if (GetComponent<CatPartHighlighter>())
        {
            highlighter = GetComponent<CatPartHighlighter>();
        }
        else
        {
            Debug.Log("Could not find highlighter on " + gameObject.name);
        }
	}

    void CalculateCombinedBounds()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (GetComponent<Renderer>())
        {
            combinedBounds = GetComponent<Renderer>().bounds;
        }
        else
        {
            combinedBounds = renderers[0].bounds;

        }

        foreach (Renderer r in renderers)
        {
            combinedBounds.Encapsulate(r.bounds);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SnapZone")
        {
            if (snapZones.Contains(other.GetComponent<CatSnapZone>())) return;
            inSnapZone = true;
            CatSnapZone z = other.GetComponent<CatSnapZone>();
            if(!z.IsZoneFull()) snapZones.Add(z);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnapZone")
        {
            snapZones.Remove(other.GetComponent<CatSnapZone>());
            if (snapZones.Count <= 0)
            {
                Destroy(previewSnapObject);
                potentialSnapPoint = null;
                inSnapZone = false;
            }
        }
    }

    public override void OnBeginHighlight()
    {
        base.OnBeginHighlight();
        if(highlighter != null) highlighter.ApplyHighlight();
    }

    public override void OnEndHighlight()
    {
        base.OnEndHighlight();
        if (highlighter != null) highlighter.RemoveHighlight();
    }

    public override void OnHoldInteractable(HandInteractionController handController)
    {
        base.OnHoldInteractable(handController);
        if (snapped) UnSnap();
    }

    public override void OnReleaseInteractable(HandInteractionController handController)
    {
        beingHeld = false;

        if (inSnapZone && potentialSnapPoint != null)
        {
            VRSystemInput.Input.leftHand.RemoveInteractableObject(this);
            VRSystemInput.Input.rightHand.RemoveInteractableObject(this);
            FinalizeSnap();
        }
        else
        {
            rb.velocity = handController.GetHandVelocity();
            rb.angularVelocity = handController.GetHandAngularVelocity();
        }

    }

    public override void OnDropInteractable(HandInteractionController handController)
    {
        base.OnDropInteractable(handController); 
    }

    CatSnapZone BestSnapZone()
    {
        float bestDist = Mathf.Infinity;
        CatSnapZone bestZone = null;

        foreach (CatSnapZone s in snapZones)
        {
            if (s.IsZoneFull()) continue;
            float dist = Vector3.Distance(transform.position, s.transform.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestZone = s;
            }
            
        }

        return bestZone;
    }

    void PreviewSnap()
    {
        CatSnapZone zone = BestSnapZone();
        if (zone == null) return;
        potentialSnapPoint = zone.BestSnapPointForSnappingObject(this);

        if (potentialSnapPoint == null) return;

        if(previewSnapObject == null)
        {
            previewSnapObject = (GameObject) Instantiate(gameObject, potentialSnapPoint.transform.position, potentialSnapPoint.transform.rotation);
            previewSnapObject.transform.parent = potentialSnapPoint.transform;
            InteractableObject i = previewSnapObject.GetComponent<InteractableObject>();
            i.OnBeginHighlight();
            DestroyImmediate(previewSnapObject.GetComponent<Rigidbody>());
            Object.DestroyImmediate(i);
            
            foreach(Collider c in previewSnapObject.GetComponentsInChildren<Collider>())
            {
                DestroyImmediate(c);
            }


        }
        else
        {
            previewSnapObject.transform.parent = potentialSnapPoint.transform;
            previewSnapObject.transform.localPosition = Vector3.zero;
            previewSnapObject.transform.localEulerAngles = Vector3.zero;
        }
    }

    void FinalizeSnap()
    {
        Debug.Log("Finalizing Snap");
        if(potentialSnapPoint == null)
        {
            Debug.LogWarning("Could not find good snap point");
        }

        snapped = true;
        snappedToPoint = potentialSnapPoint;

        Destroy(previewSnapObject);
        snappedToPoint.ValidatePartAndSnap(this);
        potentialSnapPoint = null;
    }

    void UnSnap()
    {
        snappedToPoint.UnsnapObject(this);
        snapped = false;
        snappedToPoint = null;
    }


    // Update is called once per frame
    void Update ()
    {
        if (inSnapZone && beingHeld)
        {
            PreviewSnap();
        }	
	}
}
