using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseInput
{
    public void Tick()
    {
        MouseButtonPressed();
        MouseButtonUp();
        MouseButtonDown();
    }

    public bool MouseButtonPressed()
    {
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }

    public bool MouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
            return true;
        else
            return false;
    }

    public bool MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }
}