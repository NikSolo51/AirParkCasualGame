using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterDestroy : MonoBehaviour
{
    public string destroyTag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == destroyTag)
        Destroy(this.gameObject);
    }
}
