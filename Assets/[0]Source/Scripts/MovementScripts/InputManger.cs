using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : IMoveInput
{
    
    public void ReadInput()
    { 
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }

    public float HorizontalInput { get; private set;}
    public float VerticalInput { get; private set; }
}
