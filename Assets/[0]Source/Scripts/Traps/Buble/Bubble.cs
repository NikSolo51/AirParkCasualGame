using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static BubbleBehavior bubbleBehavior;

    void Awake()
    {
        bubbleBehavior = new BubbleBehavior();
    }
}