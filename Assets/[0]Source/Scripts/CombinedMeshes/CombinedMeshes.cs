using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[BurstCompile]
public class CombinedMeshes : MonoBehaviour
{
    void Start()
        {
            Vector3 position = transform.position;
            Vector3 scale = transform.localScale;
            Quaternion rot = transform.rotation;
            
            //Get starting values
            transform.position = Vector3.zero; 
            transform.localScale = new Vector3(1,1,1);
            transform.rotation = Quaternion.identity;
            
            //Get childs meshes
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            
            //Combine Meshes
            int i = 1;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
    
                i++;
            }
            
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine,true , true);
            transform.gameObject.SetActive(true);
            
            //Get starting values
            transform.position = position;
            transform.localScale = scale;
            transform.rotation =rot;
        }
}
