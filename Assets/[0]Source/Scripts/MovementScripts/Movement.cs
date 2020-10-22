using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class Movement
{
    private readonly IMoveInput moveInput;
    private readonly Transform transformToMove;
    private readonly MovementSetting movementSetting;

    public Movement(IMoveInput moveInput, Transform transformToMove, MovementSetting movementSetting)
    {
        this.moveInput = moveInput;
        this.transformToMove = transformToMove;
        this.movementSetting = movementSetting;
    }

    public void Tick()
    {
        //transformToMove.Rotate(Vector3.up * moveInput.HorizontalInput * Time.deltaTime * movementSetting.RotationSpeed);
        
            transformToMove.position += new Vector3(0 , 0, 1) * moveInput.VerticalInput
                                             * (Time.fixedDeltaTime * movementSetting.MoveSpeed);

            if (Input.GetKey(KeyCode.Space))
                transformToMove.position = new Vector3(83.98f, 1.521f, 0.98f);


    }
}