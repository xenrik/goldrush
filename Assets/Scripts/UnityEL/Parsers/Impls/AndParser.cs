using UnityEngine;
using UnityEditor;

public class AndParser : BinaryTokenParser {
    public override string Name { get { return "and"; } }

    public AndParser() : 
        base(new DoubleCharacterParser('&')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new AndToken(symbolPos, lhs, rhs));
        } else {
            return new AndToken(symbolPos, lhs, rhs);
        }
    }
}