using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTorus {

    public static GameObject GenerateTube(float radius, int smoothness, bool fullUV, float startAngle, float endAngle) {
        GameObject go = new GameObject();
        MeshFilter filter = go.AddComponent<MeshFilter>();
        GenerateMesh(filter, radius, smoothness, fullUV, startAngle, endAngle);

        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

        return go;
    }

    public static void GenerateMesh(MeshFilter mf, float radius, int meshSmoothness, bool fullUvRange, float startAngle, float endAngle) {
        Mesh mesh = mf.mesh;
        mesh.Clear();

        int ringVertices = 24 * meshSmoothness;
        float ringVertexDa = (2 * Mathf.PI) / ringVertices;

        int ringCount = 16 * meshSmoothness;
        float ringRotateDa = (endAngle - startAngle) / ringCount;

        Vector3[] vertices = new Vector3[ringVertices * (ringCount + 1)];
        Vector3[] normals = new Vector3[vertices.Length];
        int[] triangles = new int[(vertices.Length - ringVertices) * 6];

        Quaternion ringRotation;
        Vector3 vertex;
        Vector3 ringOrigin;
        int vertexN = 0;
        int triangleN = 0;
        for (int ring = 0; ring <= ringCount; ++ring) {
            ringRotation = Quaternion.Euler(ringRotateDa * ring, 0, 0);
            ringOrigin = ringRotation * new Vector3(radius, radius, 0);

            for (int ringVertex = 0; ringVertex < ringVertices; ++ringVertex) {
                vertex.x = Mathf.Sin(ringVertexDa * ringVertex) * radius;
                vertex.y = Mathf.Cos(ringVertexDa * ringVertex) * radius;
                vertex.z = 0;

                vertex.x += radius;
                vertex.y += radius;

                if (ring != ringCount) {
                    triangles[triangleN++] = vertexN;
                    triangles[triangleN++] = ringVertex == 0 ? vertexN + (2 * ringVertices) - 1 : vertexN + ringVertices - 1;
                    triangles[triangleN++] = vertexN + ringVertices;

                    triangles[triangleN++] = vertexN;
                    triangles[triangleN++] = vertexN + ringVertices;
                    triangles[triangleN++] = (ringVertex == ringVertices - 1) ? vertexN - ringVertices + 1 : vertexN + 1;
                }

                vertex = ringRotation * vertex;
                normals[vertexN] = (vertex - ringOrigin).normalized;

                vertex.x -= radius;
                vertex.y -= radius;

                vertices[vertexN++] = vertex;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }
}
