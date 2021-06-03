using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float angle = 40;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.up, angle * Time.deltaTime);
    }
}
