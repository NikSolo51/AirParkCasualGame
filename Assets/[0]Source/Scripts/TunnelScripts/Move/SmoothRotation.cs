using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation
{
    public void Smooth(Transform currentPos, Transform target, float speed)
    {
        //Vector3 targetDir = (target.position - currentPos.position).normalized;


        //currentPos.rotation = Quaternion.Slerp(currentPos.rotation, Quaternion.LookRotation(targetDir), Time.time * speed);
        currentPos.LookAt(target.position);
       
        
    }
}