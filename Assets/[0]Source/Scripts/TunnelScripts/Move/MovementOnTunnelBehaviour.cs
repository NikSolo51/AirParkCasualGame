using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class MovementOnTunnelBehaviour : MonoBehaviour
{
    public CamultLine _camultLine;
    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> centers;
    [SerializeField] private float speed;
    public int indexOfCenterPoint;
    private SmoothRotation smoothRotation;

    public void Move(Transform player, List<GameObject> centersPoints, SmoothRotation _smoothRotation)
    {
        this.player = player;
        this.centers = centersPoints;
        this.smoothRotation = _smoothRotation;
    }

    public void Tick()
    {
        if (MovementOnTunnel.stopGame)
            return;

        if (centers != null && player != null && centers.Count > 0)
        {
            if (Vector3.Distance(player.transform.position, centers[indexOfCenterPoint].transform.position) < 0.3f)
                indexOfCenterPoint++;

            if (indexOfCenterPoint < centers.Count)
            {
                if(_camultLine.curveCoordinates.Length > 0)
                if (indexOfCenterPoint < _camultLine.curveCoordinates.Length)
                {
                    player.transform.position =
                        Vector3.MoveTowards(player.transform.position, _camultLine.curveCoordinates[indexOfCenterPoint],
                            1f * speed);

                    if (indexOfCenterPoint + 1 < centers.Count)
                        smoothRotation.Smooth(player.transform, centers[indexOfCenterPoint + 1].transform, 0.1f);
                    else
                        smoothRotation.Smooth(player.transform, centers[indexOfCenterPoint].transform, 0.5f);
                }
            }
        }
    }
}