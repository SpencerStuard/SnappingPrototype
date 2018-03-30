using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{

    public bool occupied;
    public SnappingInteractableObject snappedObject;
    GameObject imposter;


    public void CreateImposterObject(GameObject snappedObject)
    {
        imposter = Instantiate(snappedObject, transform);
        imposter.tag = "Untagged";
        DestroyImmediate(imposter.GetComponent<Rigidbody>());
        DestroyImmediate(imposter.GetComponent<InteractableObject>());
        DestroyImmediate(imposter.GetComponent<InteractableCompoundCollider>());
    }

    public void DestroyImposter()
    {
        DestroyImmediate(imposter);
    }
    

}
