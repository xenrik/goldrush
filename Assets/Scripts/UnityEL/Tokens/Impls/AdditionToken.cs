using UnityEngine;
using UnityEditor;

public class AdditionToken : BinaryToken {
    public override string Name { get { return "addition"; } }

    public AdditionToken() {
    }

    public AdditionToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }
}