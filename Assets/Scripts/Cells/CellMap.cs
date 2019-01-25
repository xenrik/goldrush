using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMap : MonoBehaviour {

    private Cell[,,] map;

    public Vector3Int Size { get; private set; }
    public Cell this[Vector3Int position] {
        get {
            if (position.x >= map.GetLength(0) ||
                position.y >= map.GetLength(1) ||
                position.z >= map.GetLength(2) ||
                position.x < 0 ||
                position.y < 0 ||
                position.z < 0) {
                return null;
            }

            return map[position.x, position.y, position.z];
        }

        set {
            if (position.x >= map.GetLength(0) ||
                position.y >= map.GetLength(1) ||
                position.z >= map.GetLength(2) ||
                position.x < 0 ||
                position.y < 0 ||
                position.z < 0) {
                return;
            }

            map[position.x, position.y, position.z] = value;
        }
    }

    private void Start() {
        Cell[] cells = GetComponentsInChildren<Cell>();
        Vector3Int size = Vector3Int.zero;
        foreach (Cell cell in cells) {
            size.x = Mathf.Max(cell.Position.x, size.x);
            size.y = Mathf.Max(cell.Position.y, size.y);
            size.z = Mathf.Max(cell.Position.z, size.z);
        }

        this.Size = size;
        map = new Cell[size.x, size.y, size.z];
        foreach (Cell cell in cells) {
            this[cell.Position] = cell;
        }
    }
}
