using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private bool StopTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(StopTrigger)
            return;
        
        if (other.GetComponent<MovementOnTunnel>())
        {
            StopTrigger = true;
            MovementOnTunnel.movementOnTunnelBehaviour.indexOfCenterPoint++;
        }
    }
}
