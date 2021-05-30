using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

[BurstCompile]
public class MovementOnTunnel : MonoBehaviour
{
    [SerializeField] private CamultLine _camultLine;
    [SerializeField] private MovementOnTunnelBehaviour _movementOnTunnelBehaviour;
    public static MovementOnTunnelBehaviour movementOnTunnelBehaviour;
    public static SmoothRotation _smoothRotation;
    public static bool stopGame = false;

    public void StopGame()
    {
        stopGame = true;
    }

    public void ContinueGame()
    {
        stopGame = false;
    }

    private void Start()
    {
        //movementOnTunnelBehaviour = new MovementOnTunnelBehaviour();
        movementOnTunnelBehaviour = _movementOnTunnelBehaviour;
        _smoothRotation = new SmoothRotation();
    }

    public void FixedUpdate()
    {
        movementOnTunnelBehaviour.Tick();
    }

    public void StartGame()
    {
        _camultLine.GeneratePoints();
        movementOnTunnelBehaviour.Move(transform, _camultLine.linePoints, _smoothRotation);
    }
}