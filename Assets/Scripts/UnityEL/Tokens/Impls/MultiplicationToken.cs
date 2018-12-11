using UnityEngine;
using UnityEditor;

public class MultiplicationToken : BinaryToken {
    public override string Name { get { return "multiplication"; } }

    public MultiplicationToken() {
    }

    public MultiplicationToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }
}