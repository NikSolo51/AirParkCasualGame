using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class MovementOnTunnelBehaviour
{
    public Transform player;
    private List<GameObject> centers;
    public int indexOfCenterPoint;
    private SmoothRotation smoothRotation;

    public void move(Transform player, List<GameObject> centersPoints, SmoothRotation _smoothRotation)
    {
        this.player = player;
        this.centers = centersPoints;
            this.smoothRotation = _smoothRotation;
    }
    
    public void Tick()
    {
        if(MovementOnTunnel.stopGame)
            return;
        
        if (centers != null && player != null)
        {
            
            if (indexOfCenterPoint < centers.Count)
            {

                if (indexOfCenterPoint < CamultLine.CurveCoordinates.Length)
                {
                    
                    player.transform.position =
                        Vector3.MoveTowards(player.transform.position, CamultLine.CurveCoordinates[indexOfCenterPoint], 1f);

                    if (indexOfCenterPoint + 1 < centers.Count)
                        smoothRotation.Smooth(player.transform, centers[indexOfCenterPoint + 1].transform, 0.1f);
                    else
                        smoothRotation.Smooth(player.transform, centers[indexOfCenterPoint].transform, 0.5f);
                }
                    
            }
        }
        
    }
}