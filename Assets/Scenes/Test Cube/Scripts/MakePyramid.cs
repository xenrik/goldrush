using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePyramid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
        Vector3[] pyramidVertices = new Vector3[4];
        int[] pyramidTriangles = new int[4 * 3];

        pyramidVertices[0] = new Vector3(Mathf.Sqrt(8f / 9f), 0, -1f / 3f) * 0.5f;
        pyramidVertices[1] = new Vector3(-Mathf.Sqrt(2f / 9f), Mathf.Sqrt(2f / 3f), -1f /3f) * 0.5f;
        pyramidVertices[2] = new Vector3(-Mathf.Sqrt(2f / 9f), -Mathf.Sqrt(2f / 3f), -1f / 3f) * 0.5f;
        pyramidVertices[3] = new Vector3(0, 0, 1) * 0.5f;

        pyramidVertices[0] = new Vector3(0, -1f / 3f, Mathf.Sqrt(8f / 9f)) * 0.5f;
        pyramidVertices[1] = new Vector3(Mathf.Sqrt(2f / 3f), -1f / 3f, -Mathf.Sqrt(2f / 9f)) * 0.5f;
        pyramidVertices[2] = new Vector3(-Mathf.Sqrt(2f / 3f), -1f / 3f, -Mathf.Sqrt(2f / 9f)) * 0.5f;
        pyramidVertices[3] = new Vector3(0, 1, 0) * 0.5f;

        int t = 0;
        int p1 = vertexN, p2 = vertexN + 1, p3 = vertexN + 2, p4 = vertexN + 3;

        pyramidTriangles[t++] = p4; pyramidTriangles[t++] = p2; pyramidTriangles[t++] = p3;
        pyramidTriangles[t++] = p4; pyramidTriangles[t++] = p1; pyramidTriangles[t++] = p2;
        pyramidTriangles[t++] = p4; pyramidTriangles[t++] = p3; pyramidTriangles[t++] = p1;
        pyramidTriangles[t++] = p1; pyramidTriangles[t++] = p3; pyramidTriangles[t++] = p2;

        Vector3[] verticesCopy = new Vector3[vertices.Length + pyramidVertices.Length];
        int[] trianglesCopy = new int[triangles.Length + pyramidTriangles.Length];

        System.Array.Copy(vertices, verticesCopy, vertices.Length);
        System.Array.Copy(pyramidVertices, 0, verticesCopy, vertices.Length, pyramidVertices.Length);

        System.Array.Copy(triangles, trianglesCopy, triangles.Length);
        System.Array.Copy(pyramidTriangles, 0, trianglesCopy, triangles.Length, pyramidTriangles.Length);

        vertices = verticesCopy;
        triangles = trianglesCopy;
        */

    }
}
