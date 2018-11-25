using UnityEditor;
using UnityEngine;

public class ShowNormals : MonoBehaviour {

    private static bool drawGizmos;

    private void OnDrawGizmos() {
        drawGizmos = true;
    }

    private void OnDrawGizmosSelected() {
        drawGizmos = true;
    }

    [DrawGizmo(GizmoType.Selected)]
    private static void DrawGizmosSelected(MeshFilter filter, GizmoType gizmoType) {
        if (!drawGizmos) {
            return;

        }
        /*
        MeshFilter filter = target as MeshFilter;
        if (filter == null) {
            return;
        }
        */

        Mesh mesh = filter.sharedMesh;
        if (mesh == null) {
            return;
        }

        Handles.matrix = filter.transform.localToWorldMatrix;
        Handles.color = Color.yellow;
        for (int i = 0; i < mesh.vertexCount; i++) {
            Handles.DrawLine(mesh.vertices[i], mesh.vertices[i] + (mesh.normals[i] * 0.1f));
        }

        drawGizmos = false;
    }
}