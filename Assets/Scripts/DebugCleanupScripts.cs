using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCleanupScripts : MonoBehaviour
{
    public Material matToAssign;


	// Use this for initialization
	void Start () {
		
	}
	
    [ContextMenu("Assign Material To Children")]
    public void AssignDefaultMatToAllChildren()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            for(int i = 0; i < r.sharedMaterials.Length; i ++)
            {
                r.sharedMaterials[i] = matToAssign;
            }
        }
    }

    [ContextMenu("Delete all rigidbodies and joints")]
    public void CleanupRigidbodiesAndJoints()
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        Joint[] joints = GetComponentsInChildren<Joint>();
        foreach(Rigidbody r in rb)
        {
            DestroyImmediate(r);
        }

        foreach(Joint j in joints)
        {
            DestroyImmediate(j);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
