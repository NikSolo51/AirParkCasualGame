using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Burst;

[BurstCompile]
public class GetVertexPositions : MonoBehaviour
{
    private Mesh mesh;
    public List<Vector3> vertexList = new List<Vector3>();
    public static Dictionary<int, List<Vector3>> vertexListByIdDictionary = new Dictionary<int, List<Vector3>>();

    private int id;

    //0
    private void Awake()
    {
        id = transform.GetSiblingIndex();

        mesh = GetComponent<MeshFilter>().mesh;

        TransformVertexLocalPositionToWorld();

        vertexListByIdDictionary.Add(id, vertexList);
        
        SortDictionary();
    }

    public void SortDictionary()
    {
        var orderedKeyValuePairs = vertexListByIdDictionary.OrderBy(key => key.Key);
        vertexListByIdDictionary =
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