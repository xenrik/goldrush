using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMap : MonoBehaviour {

    private GameObject[,,] map;

    public Vector3Int Size { get; private set; }
    public GameObject this[Vector3Int position] {
        get {
            return map[position.x, position.y, position.z];
        }

        set {
            map[position.x, position.y, position.z] = value;
        }
    }

    private void Start() {
        PipeNode[] nodes = GetComponentsInChildren<PipeNode>();
        Vector3Int size = Vector3Int.zero;
        foreach (PipeNode node in nodes) {
            size.x = Mathf.Max(node.Position.x, size.x);
            size.y = Mathf.Max(node.Position.y, size.y);
            size.z = Mathf.Max(node.Position.z, size.z);
        }

        this.Size = size;
        map = new GameObject[size.x, size.y, size.z];
        foreach (PipeNode node in nodes) {
            this[node.Position] = node.gameObject;
        }
    }
}
