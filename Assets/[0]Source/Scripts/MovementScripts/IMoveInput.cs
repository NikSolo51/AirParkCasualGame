using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveInput
{
    void ReadInput();
    float HorizontalInput { get; }
    float VerticalInput { get; }
    
}
