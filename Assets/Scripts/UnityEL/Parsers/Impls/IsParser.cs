using UnityEngine;
using UnityEditor;

public class IsParser : BinaryTokenParser {
    public override string Name { get { return "is"; } }

    public IsParser() : 
        base(new WordTokenParser(true, "is")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new IsToken(symbolPos, lhs, rhs));
        } else {
            return new IsToken(symbolPos, lhs, rhs);
        }
    }
}