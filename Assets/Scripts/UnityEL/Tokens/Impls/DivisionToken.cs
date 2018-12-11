using UnityEngine;
using UnityEditor;

public class DivisionToken : BinaryToken {
    public override string Name { get { return "division"; } }

    public DivisionToken() {
    }

    public DivisionToken(int position, TokenImpl lhs, TokenImpl rhs) : base(position, lhs, rhs) {
    }
}