using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractableCompoundCollider : MonoBehaviour
{

    public Dictionary<Collider, int> collisions = new Dictionary<Collider, int>();
    public Collider[] colliders;
    public int[] collisionCount;
    InteractableObject interactable;

	// Use this for initialization
	void Start ()
    {
        interactable = GetComponent<InteractableObject>();
	}
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller" || other.gameObject.tag == "SnapZone")
        {
            if (collisions.ContainsKey(other))
            {
                collisions[other]++;
            }
            else
            {
                collisions.Add(other, 1);
                interactable.OnCompoundTriggerEnter(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller" || other.gameObject.tag == "SnapZone")
        {
            if (collisions.ContainsKey(other))
            {
                collisions[other]--;
                if (collisions[other] <= 0)
                {
                    collisions.Remove(other);
                    interactable.OnCompoundTriggerExit(other);
                }
            }
            else
            {
                Debug.LogError("This shouldn't happen");
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
        colliders = collisions.Keys.ToArray();
        collisionCount = collisions.Values.ToArray();
    }
}
