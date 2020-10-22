using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<AddPeoplesToLinkList>())
        {
            other.transform.SetParent(this.transform);
        }
    }
}