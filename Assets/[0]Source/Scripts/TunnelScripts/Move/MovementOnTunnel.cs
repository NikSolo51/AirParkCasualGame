using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

[BurstCompile]
public class MovementOnTunnel : MonoBehaviour
{
   public static MovementOnTunnelBehaviour movementOnTunnelBehaviour;
   public static SmoothRotation _smoothRotation;
   public static bool stopGame = false;
   public Transform startPos;

   public void StopGame()
   {
       stopGame = true;
   }
   
   public void ContinueGame()
   {
       stopGame = false;
   }
   
   private void Start ()
   {
        movementOnTunnelBehaviour = new MovementOnTunnelBehaviour();
        _smoothRotation = new SmoothRotation();
   }

   public void FixedUpdate()
   {
       movementOnTunnelBehaviour.Tick();
   }

   public void startGame()
   {
       CamultLine.GeneratePoints();
       movementOnTunnelBehaviour.move(transform , CamultLine.LinePoints , _smoothRotation);
   }

}