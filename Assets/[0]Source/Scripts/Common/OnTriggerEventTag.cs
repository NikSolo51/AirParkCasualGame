using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEventTag : MonoBehaviour
{
    public string tag;
    public UnityEvent OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            Debug.Log("enter");
            OnEnter.Invoke();
        }
    }
}
