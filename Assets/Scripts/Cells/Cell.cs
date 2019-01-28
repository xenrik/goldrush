using UnityEngine;

public class Cell : MonoBehaviour {
    public enum CellType {
        EMPTY,

        START,
        CORNER,
        END
    }

    public CellType Type;
    public GameObject PipeAnchor;

    public Vector3Int PreviousOffset;
    public Vector3Int NextOffset;

    /**
     * Converts our transform position into a Cell Position
     */
    public Vector3Int Position {
        get {
            return new Vector3Int((int)transform.localPosition.x, (int)transform.localPosition.y, (int)transform.localPosition.z);
        }
    }
}
