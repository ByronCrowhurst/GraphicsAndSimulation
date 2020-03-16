using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [Range(2, 256)] public int resolution = 10;
    [SerializeField, HideInInspector] MeshFilter[] meshFilters;
    SphereFace[] faces;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

   
    void Initialize()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        faces = new SphereFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                Debug.Log("Mesh Filter: " + i);
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Custom/Fireball"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();


            }
            faces[i] = new SphereFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        int sphereCount = 0;
        foreach (SphereFace sphere in faces)
        {

            Debug.Log(sphereCount);
            sphere.CreateMesh();
        }

    }
}
