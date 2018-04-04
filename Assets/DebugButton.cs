using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugButton : MonoBehaviour
{
    public UnityEvent clickEvent = new UnityEvent();
    bool canTrigger = true;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Controller")
        {
            if (canTrigger) StartCoroutine(Clicking());
        }
    }

    IEnumerator Clicking()
    {
        canTrigger = false;
        mat.color = Color.red;
        yield return new WaitForSeconds(1f);
        clickEvent.Invoke();

        yield return new WaitForSeconds(2f);
        canTrigger = true;

        mat.color = Color.green;
        yield break;
    }

}
