using UnityEngine;
using UnityEditor;

public class NotEqualsParser : BinaryTokenParser {
    public override string Name { get { return "notEquals"; } }

    public NotEqualsParser() : 
        base(new StringTokenParser("!=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new NotEqualsToken(symbolPos, lhs, rhs));
        } else {
            return new NotEqualsToken(symbolPos, lhs, rhs);
        }
    }
}