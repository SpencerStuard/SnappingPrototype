using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VRSystemInput : MonoBehaviour
{
    //singleton access
    public static VRSystemInput Input;
    public void Awake()
    {
        if(VRSystemInput.Input == null)
        {
            Input = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public enum Hand {Left, Right}

    //Controller Info
    public abstract Vector3 GetVelocity(Hand h);
    public abstract Vector3 GetAngularVelocity(Hand h);

    public Vector3 LeftControllerVelocity;
    public Vector3 LeftControllerAngularVelocity;
    public Vector3 RightControllerVelocity;
    public Vector3 RightControllerAngularVelocity;

    //pickup button
    public bool LeftPickupPressed = false;
    public bool RightPickupPressed = false;
    public bool LeftPickupDown = false;
    public bool RightPickupDown = false;
    public bool LeftPickupUp = false;
    public bool RightPickupUp = false;

    public abstract float GetPickupInputAxis(Hand h);
    public abstract bool GetPickupInput(Hand h);
    public abstract bool GetPickupDown(Hand h);
    public abstract bool GetPickupUp(Hand h);

    //interact button
    public bool LeftInteractPressed = false;
    public bool RightInteractPressed = false;
    public bool LeftInteractDown = false;
    public bool RightInteractDown = false;
    public bool LeftInteractUp = false;
    public bool RightInteractUp = false;

    public abstract bool GetInteractButton(Hand h);
    public abstract bool GetInteractDown(Hand h);
    public abstract bool GetInteractUp(Hand h);

    //poll inputs
    public abstract void PollInputs();
}
