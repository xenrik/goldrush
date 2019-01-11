using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNode : MonoBehaviour {
    public enum NodeType {
        START,
        CORNER,
        END
    }

    public NodeType Type;

    public PipeNode PreviousNode;
    public PipeNode NextNode;
}
