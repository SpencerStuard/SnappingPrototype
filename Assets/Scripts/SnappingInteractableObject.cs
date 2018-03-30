using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingInteractableObject : InteractableObject
{

    bool snapped = false;
    SnapPoint snappedToPoint;
    Transform presnapParent;

    public bool inSnapZone = false;
    public List<SnapZone> snapZones = new List<SnapZone>();
    public SnapPoint potentialSnapPoint;
    public GameObject previewSnapObject;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        presnapParent = transform.parent;
	}

    public override void OnCompoundTriggerEnter(Collider other)
    {
        base.OnCompoundTriggerEnter(other);

        if (other.gameObject.tag == "SnapZone")
        {
            if (snapZones.Contains(other.GetComponent<SnapZone>())) return;
            inSnapZone = true;
            snapZones.Add(other.GetComponent<SnapZone>());
        }
    }

    public override void OnCompoundTriggerExit(Collider other)
    {
        base.OnCompoundTriggerExit(other);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Controller")
        {
            Debug.Log("Detected Coliision with controller");
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (colliderMode == ColliderModeType.Compound) return;
        if (other.gameObject.tag == "Controller")
        {
            other.GetComponent<HandInteractionController>().AddInteractableObject(this);
        }

        if(other.gameObject.tag == "SnapZone")
        {
            if (snapZones.Contains(other.GetComponent<SnapZone>())) return;
            inSnapZone = true;
            snapZones.Add(other.GetComponent<SnapZone>());
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (colliderMode == ColliderModeType.Compound) return;
        if (other.gameObject.tag == "Controller")
        {
            other.GetComponent<HandInteractionController>().RemoveInteractableObject(this);
        }

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
            previewSnapObject = (GameObject) Instantiate(gameObject, potentialSnapPoint.transform);
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
        transform.parent = potentialSnapPoint.transform;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        snapped = true;
        snappedToPoint = potentialSnapPoint;

        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = 9;
        }
        rb.isKinematic = true;

        snappedToPoint.occupied = true;
        snappedToPoint.snappedObject = this;

        potentialSnapPoint = null;
    }

    void UnSnap()
    {
        snappedToPoint.occupied = false;
        snappedToPoint.snappedObject = null;

        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = 8;
        }
        rb.isKinematic = false;


        snapped = false;
        snappedToPoint = null;
        transform.parent = presnapParent;
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
