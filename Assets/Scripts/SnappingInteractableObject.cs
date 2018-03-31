using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingInteractableObject : InteractableObject
{

    bool snapped = false;
    SnapPoint snappedToPoint;
    public Transform presnapParent;

    public bool inSnapZone = false;
    public List<SnapZone> snapZones = new List<SnapZone>();
    public SnapPoint potentialSnapPoint;
    public GameObject previewSnapObject;
    public Bounds combinedBounds;

    public bool isDynamic = false;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        presnapParent = transform.parent;
        CalculateCombinedBounds();
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
            if (snapZones.Contains(other.GetComponent<SnapZone>())) return;
            inSnapZone = true;
            snapZones.Add(other.GetComponent<SnapZone>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnapZone")
        {
            snapZones.Remove(other.GetComponent<SnapZone>());
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
    }

    public override void OnEndHighlight()
    {
        base.OnEndHighlight();
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
            FinalizeSnap();
            VRSystemInput.Input.leftHand.RemoveInteractableObject(this);
            VRSystemInput.Input.rightHand.RemoveInteractableObject(this);
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

    SnapZone BestSnapZone()
    {
        float bestDist = Mathf.Infinity;
        SnapZone bestZone = null;

        foreach (SnapZone s in snapZones)
        {
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
        SnapZone zone = BestSnapZone();
        potentialSnapPoint = zone.BestSnapPointForSnappingObject(this);

        if (potentialSnapPoint == null) return;

        if(previewSnapObject == null)
        {
            previewSnapObject = (GameObject) Instantiate(gameObject, potentialSnapPoint.transform.position, potentialSnapPoint.transform.rotation, potentialSnapPoint.transform);
            InteractableObject i = previewSnapObject.GetComponent<InteractableObject>();
            i.rb.detectCollisions = false;
            Object.DestroyImmediate(i);
            previewSnapObject.tag = "Untagged";
            if (zone.highlighter != null) zone.highlighter.ApplyHighlightToGameObject(previewSnapObject);
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
        if(potentialSnapPoint == null)
        {
            Debug.LogWarning("Could not find good snap point");
        }

        Destroy(previewSnapObject);
        snappedToPoint = potentialSnapPoint;
        snappedToPoint.SnapObject(this, isDynamic);
        snapped = true;
        potentialSnapPoint = null;
    }

    void UnSnap()
    {
        snappedToPoint.UnsnapObject(this, isDynamic);
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
