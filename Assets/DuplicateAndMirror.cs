using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateAndMirror : MonoBehaviour {

    public Transform target;
    public Transform destination;

    [ContextMenu("Mirror Transform")]
	public void DuplicateAndMirrorTransform()
    {
        GameObject g = GameObject.Instantiate(target.gameObject, target.parent);
        
        g.transform.position = target.transform.position;
        g.transform.rotation = target.transform.rotation;
        g.transform.localScale = target.transform.localScale;
        return;
        g.transform.parent = transform;
        Vector3 s = gameObject.transform.localScale;
        s.Scale(Vector3.left);
        transform.localScale = s;
        g.transform.parent = destination;
        g.transform.localScale = Vector3.one;
    }

}
