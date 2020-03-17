using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFace
{
    Mesh mesh;
    int resolution;
    Vector3 up;

    Vector3 axisA;
    Vector3 axisB;

    public SphereFace(Mesh mesh, int resolution, Vector3 up)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.up = up;

        axisA = new Vector3(up.y, up.z, up.x);
        axisB = Vector3.Cross(up, axisA);

    }
    public void CreateMesh()
    {
        Vector3[] verts = new Vector3[resolution * resolution];
        Vector2[] uvs = new Vector2[resolution * resolution];
        int[] tri = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;


        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int iter = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnCube = up + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                Vector3 pointOnSphere = pointOnCube.normalized;
                

                verts[iter] = pointOnSphere;

                if (x != resolution - 1 && y != resolution - 1)
                {
                    tri[triIndex] = iter;
                    tri[triIndex + 1] = iter + resolution + 1;
                    tri[triIndex + 2] = iter + resolution;
                    
                    tri[triIndex + 3] = iter;
                    tri[triIndex + 4] = iter + 1;
                    tri[triIndex + 5] = iter + resolution + 1;
                    triIndex += 6;
                }

                uvs[iter] = new Vector2((float)x / (float)resolution, (float)y / (float)resolution);

            }
        }
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tri;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }
}
