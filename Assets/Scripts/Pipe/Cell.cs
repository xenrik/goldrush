using UnityEngine;

public class Cell : MonoBehaviour {
    public enum CellType {
        EMPTY,

        START,
        CORNER,
        END
    }

    public CellType Type;

    public Vector3Int PreviousOffset;
    public Vector3Int NextOffset;
}
