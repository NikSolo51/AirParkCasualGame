using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovementOnTunnel>())
        {
           MovementOnTunnel.movementOnTunnelBehaviour.indexOfCenterPoint++;
        }
    }
}
