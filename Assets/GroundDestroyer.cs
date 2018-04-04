using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDestroyer : MonoBehaviour
{
    public float destroyTime = 4f;
    public List<GameObject> objsToDestroy = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Interactable")
        {
            if (!objsToDestroy.Contains(collision.transform.root.gameObject))
            {
                objsToDestroy.Add(collision.transform.root.gameObject);
                Invoke("DestroyLast", destroyTime);
            }
        }
    }

    public void DestroyLast()
    {
        GameObject go = objsToDestroy[objsToDestroy.Count - 1];
        objsToDestroy.Remove(go);
        DestroyImmediate(go);
    }

}
