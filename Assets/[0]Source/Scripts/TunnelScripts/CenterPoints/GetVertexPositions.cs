using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Burst;

[BurstCompile]
public class GetVertexPositions : MonoBehaviour
{
    public VertexManager VertexManager;
    public List<Vector3> vertexList = new List<Vector3>();
    private Mesh mesh;
   

    private int id;

    //0
    private void Awake()
    {
        id = transform.GetSiblingIndex();

        mesh = GetComponent<MeshFilter>().mesh;

        TransformVertexLocalPositionToWorld();

        VertexManager.vertexListByIdDictionary.Add(id, vertexList);
        
        SortDictionary();
    }

    public void SortDictionary()
    {
        var orderedKeyValuePairs = VertexManager.vertexListByIdDictionary.OrderBy(key => key.Key);
        VertexManager.vertexListByIdDictionary =
            orderedKeyValuePairs.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
    }


    private void TransformVertexLocalPositionToWorld()
    {
        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            vertexList.Add(localToWorld.MultiplyPoint3x4(mesh.vertices[i]));
        }
    }
}