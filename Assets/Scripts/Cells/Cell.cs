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

    /**
     * Converts our transform position into a Vector3Int
     */
    public Vector3Int Position {
        get {
            return new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        }
    }
}
