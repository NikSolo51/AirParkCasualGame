using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
    public float delay = 2;
    public UnityEvent DelEvent;

    public void Func()
    {
        Invoke("StartEvent", delay);
    }

    private void StartEvent()
    {
        DelEvent.Invoke();
    }
}