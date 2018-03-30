using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSystemInputOculus : VRSystemInput {

    //pickup button

    public override float GetPickupInputAxis(Hand h)
    {
        if(h == Hand.Left)
        {
            return OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        }
        else if(h == Hand.Right)
        {
            return OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        }

        return 0f;
    }

    public override bool GetPickupInput(Hand h)
    {
        return GetPickupInputAxis(h) > 0.6f;
    }

    public override bool GetPickupDown(Hand h)
    {
        if (h == Hand.Left)
        {
            return LeftPickupDown;
        }
        else
        {
            return RightPickupDown;
        }
    }

    public override bool GetPickupUp(Hand h)
    {
        if (h == Hand.Left)
        {
            return LeftPickupUp;
        }
        else
        {
            return RightPickupUp;
        }
    }

    //interact button
    public override bool GetInteractButton(Hand h)
    {
        if (h == Hand.Left)
        {
            return OVRInput.Get(OVRInput.RawButton.X);
        }
        else
        {
            return OVRInput.Get(OVRInput.RawButton.A);
        }
    }

    public override bool GetInteractDown(Hand h)
    {
        if (h == Hand.Left)
        {
            return LeftInteractDown;
        }
        else
        {
            return RightInteractDown;
        }
    }

    public override bool GetInteractUp(Hand h)
    {
        if (h == Hand.Left)
        {
            return LeftInteractUp;
        }
        else
        {
            return RightInteractUp;
        }
    }


    //Updating Inputs
    public override void PollInputs()
    {
        //pickup buttons
        LeftPickupDown = !LeftPickupPressed && GetPickupInput(Hand.Left);
        LeftPickupUp = LeftPickupPressed && !GetPickupInput(Hand.Left);
        LeftPickupPressed = GetPickupInput(Hand.Left);

        RightPickupDown = !RightPickupPressed && GetPickupInput(Hand.Right);
        RightPickupUp = RightPickupPressed && !GetPickupInput(Hand.Right);
        RightPickupPressed = GetPickupInput(Hand.Right);

        //interact buttons
        LeftInteractDown = !LeftInteractPressed && GetInteractButton(Hand.Left);
        LeftInteractUp = LeftInteractPressed && !GetInteractButton(Hand.Left);
        LeftInteractPressed = GetInteractButton(Hand.Left);

        RightInteractDown = !RightInteractPressed && GetInteractButton(Hand.Right);
        RightInteractUp = RightInteractPressed && !GetInteractButton(Hand.Right);
        RightInteractPressed = GetInteractButton(Hand.Right);
    }

    private void Update()
    {
        PollInputs();
    }

    public override Vector3 GetVelocity(Hand h)
    {
        if(h == Hand.Left)
        {
            return LeftControllerVelocity;
        }
        else
        {
            return RightControllerVelocity;
        }
    }

    public override Vector3 GetAngularVelocity(Hand h)
    {
        if (h == Hand.Left)
        {
            return LeftControllerAngularVelocity;
        }
        else
        {
            return RightControllerAngularVelocity;
        }
    }

    private void FixedUpdate()
    {
        LeftControllerVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        RightControllerVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

        LeftControllerAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch);
        RightControllerAngularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
    }
}
