using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;


public class MovementPerson : MonoBehaviour
{
    [SerializeField] private MovementSetting movementSetting;
    private IMoveInput moveInput;
    private Movement movement;


    //2
    private void Start()
    {
        moveInput = new InputManger();
        movement = new Movement(moveInput, transform, movementSetting);
    }

    public void FixedUpdate()
    {
        moveInput.ReadInput();
        movement.Tick();
    }
}