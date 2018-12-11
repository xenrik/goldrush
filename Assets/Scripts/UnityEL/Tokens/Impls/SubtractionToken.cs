using UnityEngine;
using UnityEditor;

public class SubtractionToken : BinaryToken {
    public override string Name { get { return "subtraction"; } }

    public SubtractionToken() {
    }

    public SubtractionToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }
}