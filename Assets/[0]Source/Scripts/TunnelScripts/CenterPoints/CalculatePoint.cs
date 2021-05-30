using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[BurstCompile]
public class CalculatePoint : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private List<Vector3> centerPoints = new List<Vector3>();
    public List<GameObject> points = new List<GameObject>();
    [SerializeField] private bool renderBox = false;

    private void Start()
    {
        CalculateCenterPointOfTunnel(GetVertexPositions.vertexListByIdDictionary);
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
            GameObject centerPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
            centerPoint.transform.SetParent(parent.transform);
            centerPoint.name = " center point " +  j  ;
            centerPoint.GetComponent<BoxCollider>().size = new Vector3(1 ,11,7 );
            centerPoint.GetComponent<MeshRenderer>().enabled = renderBox;
            centerPoint.transform.position = centerPoints[j];
            centerPoint.GetComponent<BoxCollider>().isTrigger = true;
            points.Add(centerPoint);
        }
    }
}