using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class AirWaves : MonoBehaviour
{
    public float speedX;
    public float speedY;
    private float curveX;
    private float curveY;

    private void Start()
    {
        curveX = GetComponent<MeshRenderer>().material.mainTextureOffset.x;
        curveY = GetComponent<MeshRenderer>().material.mainTextureOffset.y;
    }

    private void Update()
    {
        curveX += Time.deltaTime * speedX;
        curveY += Time.deltaTime * speedY;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curveX,curveY));
    }
}
