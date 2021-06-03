using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public class CalculatePoint : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    [SerializeField] public VertexManager VertexManager;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject centerPointPrefab;
    [SerializeField] private List<Vector3> centerPoints = new List<Vector3>();
    [SerializeField] private bool renderBox = false;

    private void Start()
    {
        CalculateCenterPointOfTunnel(VertexManager.vertexListByIdDictionary);
    }

      void CalculateCenterPointOfTunnel(Dictionary<int , List<Vector3>> vertexListByIdDictionary)
    {
        foreach (var getVertexPosition  in vertexListByIdDictionary)
        {
            int i = 0;
            float x = 0;
            float y = 0;
            float z = 0;
            for (int j = 0; j < getVertexPosition.Value.Count; j++)
            {
                x += getVertexPosition.Value[j].x;
                y += getVertexPosition.Value[j].y;
                z += getVertexPosition.Value[j].z;
                i++;
                
                if (i == getVertexPosition.Value.Count)
                {
                    i = 0;
                    centerPoints.Add(new Vector3(x / getVertexPosition.Value.Count, y / getVertexPosition.Value.Count, z / getVertexPosition.Value.Count));
                    x = 0;
                    y = 0;
                    z = 0;
                }
            }
        }

        for (int j = 0; j < centerPoints.Count; j++)
        {
            GameObject centerPoint = Instantiate(centerPointPrefab,centerPoints[j],quaternion.identity,parent.transform);
            centerPoint.name = " center point " +  j  ;
            centerPoint.GetComponent<MeshRenderer>().enabled = renderBox;
            points.Add(centerPoint);
        }
    }
}