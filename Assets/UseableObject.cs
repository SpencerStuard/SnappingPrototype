using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseableObject : MonoBehaviour
{

    public bool usePickupButton = false;
    public bool useInteractButton = true;
    public bool useable = true;

    public UnityEvent OnUseObject = new UnityEvent();



    public virtual void UseObject(HandActionController actionController)
    {
        OnUseObject.Invoke();
    }

}
