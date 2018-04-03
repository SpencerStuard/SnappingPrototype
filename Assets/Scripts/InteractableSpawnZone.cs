using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawnZone : MonoBehaviour {

    public GameObject interactableToSpawn;
    public Transform spawnParent;

    public bool leftControllerInZone = false;
    public bool rightControllerInZone = false;

    HandInteractionController leftHand;
    HandInteractionController rightHand;
    VRSystemInput input;

    private void Start()
    {
        input = VRSystemInput.Input;
        leftHand = input.leftHand;
        rightHand = input.rightHand;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Controller")
        {
            HandInteractionController hand = other.gameObject.GetComponent<HandInteractionController>();
            HandEnterZone(hand);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            HandInteractionController hand = other.gameObject.GetComponent<HandInteractionController>();
            HandExitZone(hand);
        }
    }

    void HandEnterZone(HandInteractionController h)
    {
        if (h.hand == VRSystemInput.Hand.Left) leftControllerInZone = true;
        else if (h.hand == VRSystemInput.Hand.Right) rightControllerInZone = true;
    }

    void HandExitZone(HandInteractionController h)
    {
        if (h.hand == VRSystemInput.Hand.Left) leftControllerInZone = false;
        else if (h.hand == VRSystemInput.Hand.Right) rightControllerInZone = false;
    }

    void SpawnObjectForController(HandInteractionController h)
    {
        GameObject spawnedInteractable = Instantiate(interactableToSpawn, h.transform.position, Quaternion.identity, spawnParent);
        h.HoldSpawnedObject(spawnedInteractable);
    }

    private void Update()
    {
        if (leftControllerInZone)
        {
            if (input.GetInteractDown(VRSystemInput.Hand.Left) && leftHand.heldObject == null)
            {
                SpawnObjectForController(leftHand);
            }
        }

        if (rightControllerInZone)
        {
            if (input.GetInteractDown(VRSystemInput.Hand.Right) && rightHand.heldObject == null)
            {
                Debug.Log("Spawned");
                SpawnObjectForController(rightHand);
            }
        }
    }
}
