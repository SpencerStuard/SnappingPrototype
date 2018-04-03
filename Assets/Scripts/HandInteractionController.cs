using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionController : MonoBehaviour
{
    public VRSystemInput.Hand hand = VRSystemInput.Hand.Left;

    public List<InteractableObject> interactablesInRange = new List<InteractableObject>();
    Dictionary<InteractableObject, int> interactableDictionary = new Dictionary<InteractableObject, int>();

    public Transform holdPoint;
    public InteractableObject heldObject;
    public FixedJoint tempJoint;

    VRSystemInput Input;

    private void Awake()
    {
        if (holdPoint == null) holdPoint = transform;
    }

    private void Start()
    {
        Input = VRSystemInput.Input;
    }

    public Vector3 GetHandVelocity()
    {
        return Input.GetVelocity(hand);
    }

    public Vector3 GetHandAngularVelocity()
    {
        return Input.GetAngularVelocity(hand);
    }

    public bool isHoldingObject()
    {
        return heldObject != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            InteractableObject io = null;
            if (other.gameObject.GetComponent<InteractableObject>())
            {
                io = other.gameObject.GetComponent<InteractableObject>();
            }
            else
            {
                io = other.gameObject.GetComponentInParent<InteractableObject>();
            }

            if(io != null)
            {
                if (interactableDictionary.ContainsKey(io))
                {
                    interactableDictionary[io]++;
                }
                else
                {
                    AddInteractableObject(io);
                }
            }
            else
            {
                Debug.LogError("Could not find interactable object for collider");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            InteractableObject io = null;
            if (other.gameObject.GetComponent<InteractableObject>())
            {
                io = other.gameObject.GetComponent<InteractableObject>();
            }
            else
            {
                io = other.gameObject.GetComponentInParent<InteractableObject>();
            }

            if (io != null)
            {
                if (interactableDictionary.ContainsKey(io))
                {
                    interactableDictionary[io]--;
                    if(interactableDictionary[io] <= 0)
                    {
                        RemoveInteractableObject(io);
                    }
                }
                else
                {
                    Debug.LogWarning("Untracked Interactable left controller Trigger zone");
                }
            }
            else
            {
                Debug.LogError("Could not find interactable object for collider");
            }

        }
    }

    private int CompareInteractableDistance(InteractableObject o1, InteractableObject o2)
    {
        float d1 = Vector3.Distance(gameObject.transform.position, o1.transform.position);
        float d2 = Vector3.Distance(gameObject.transform.position, o2.transform.position);

        return d1.CompareTo(d2);
    }

    public void AddInteractableObject(InteractableObject o)
    {
        interactablesInRange.Add(o);
        interactableDictionary.Add(o, 1);
    }

    public void RemoveInteractableObject(InteractableObject o)
    {
        if (interactablesInRange.Contains(o) && interactableDictionary.ContainsKey(o))
        {
            interactablesInRange.Remove(o);
            interactableDictionary.Remove(o);
        }
    }

    private void OnJointBreak(float breakForce)
    {
        heldObject = null;    
    }

    void CreateTempJoint()
    {
        tempJoint = gameObject.AddComponent<FixedJoint>();
        tempJoint.breakForce = 4000f;
        tempJoint.breakTorque = 4000f;
    }

    void RemoveTempJoint()
    {
        Object.DestroyImmediate(tempJoint);
    }

    public void HoldSpawnedObject(GameObject g)
    {
        InteractableObject o = g.GetComponent<InteractableObject>();
        if (o.rb == null) o.rb = o.GetComponent<Rigidbody>();
        Debug.Log("Hold Spawned Object " + g.name);
        HoldObject(o);
    }

    public void HoldObject(InteractableObject io)
    {
        io.OnHoldInteractable(this);

        CreateTempJoint();
        heldObject = io;
        tempJoint.connectedBody = io.rb;
    }

    public void ReleaseHeldObject()
    {
        heldObject.OnReleaseInteractable(this);
        RemoveTempJoint();

        heldObject = null;
    }

    void Update()
    {
        if (interactablesInRange.Count > 0)
        {
            interactablesInRange.Sort(CompareInteractableDistance);

            if (Input.GetPickupDown(hand))
            {
                InteractableObject o = null;
                foreach(InteractableObject i in interactablesInRange)
                {
                    if (i.usePickupButton)
                    {
                        o = i;
                        break;
                    } 
                }

                if(o != null) HoldObject(o);
            }

            if (Input.GetInteractDown(hand))
            {
                InteractableObject o = null;
                foreach (InteractableObject i in interactablesInRange)
                {
                    if (i.useInteractButton)
                    {
                        o = i;
                        break;
                    }
                }

                if (o != null) HoldObject(o);
            }
        }

        if (heldObject != null)
        {
            if (Input.GetPickupUp(hand))
            {
                if(heldObject.usePickupButton) ReleaseHeldObject();
            }

            if (Input.GetInteractUp(hand))
            {
                if (heldObject.useInteractButton) ReleaseHeldObject();
            }
        }

    }
}
