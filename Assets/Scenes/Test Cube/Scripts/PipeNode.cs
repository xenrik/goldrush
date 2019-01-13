using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNode : MonoBehaviour {
    public enum NodeType {
        START,
        CORNER,
        END,

        EMPTY
    }

    public NodeType Type;
    public Vector3Int Position;

    public PipeNode PreviousNode;
    public PipeNode NextNode;
}
