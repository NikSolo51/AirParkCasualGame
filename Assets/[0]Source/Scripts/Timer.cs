using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerStart = 1;
    
    void Update()
    {
        if (timerStart <= 0)
            timerStart = 1;
        
            timerStart -= Time.deltaTime;
    }
    
}
