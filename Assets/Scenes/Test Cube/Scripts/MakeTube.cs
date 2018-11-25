using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTube {

    public static GameObject GenerateTube(float length, float radius, int smoothness, bool fullUV) {
        GameObject go = new GameObject();
        MeshFilter filter = go.AddComponent<MeshFilter>();
        Make(filter, length, radius, smoothness,  fullUV);

        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Diffuse"));

        return go;
    }

    private static int Wrap(int i, int max, int reduce) {
        while (i > max) {
            i -= reduce;
        }

        return i;
    }

    private static void Make(MeshFilter mf, float length, float radius, int meshSmoothness, bool fullUvRange) {
        Mesh mesh = mf.mesh;
        mesh.Clear();

        int n = 24 * meshSmoothness;
        float da = (2 * Mathf.PI) / n;

        Vector3[] vertices = new Vector3[n * 2];
        int[] triangles = new int[vertices.Length * 3];

        for (int p = 0; p < n; ++p) {
            float x = Mathf.Sin(da * p) * radius;
            float y = Mathf.Cos(da * p) * radius;
            vertices[p] = new Vector3(x, y, 0);
            vertices[p + n] = new Vector3(x, y, length);

            triangles[p * 6] = p;
            triangles[p * 6 + 1] = p + n - 1;
            triangles[p * 6 + 2] = Wrap(p + n, 2 * n, n);

            triangles[p * 6 + 3] = p;
            triangles[p * 6 + 4] = Wrap(p + n, 2 * n, n);
            triangles[p * 6 + 5] = Wrap(p + 1, n, n);
        }

        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i < n; ++i) {
            normals[i] = vertices[i].normalized;
            normals[i + n] = normals[i];
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }
}
