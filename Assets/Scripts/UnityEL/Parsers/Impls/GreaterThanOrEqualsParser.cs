using UnityEngine;
using UnityEditor;

public class GreaterThanOrEqualsParser : BinaryTokenParser {
    public override string Name { get { return "greaterThanOrEquals"; } }

    public GreaterThanOrEqualsParser() : 
        base(new StringTokenParser(">=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new GreaterThanOrEqualsToken(symbolPos, lhs, rhs));
        } else {
            return new GreaterThanOrEqualsToken(symbolPos, lhs, rhs);
        }
    }
}