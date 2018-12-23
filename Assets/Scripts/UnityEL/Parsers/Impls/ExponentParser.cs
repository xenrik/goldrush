using UnityEngine;
using UnityEditor;

public class ExponentParser : BinaryTokenParser {
    public override string Name { get { return "exponent"; } }

    public ExponentParser() : 
        base(new DoubleCharacterParser('*')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ExponentToken(symbolPos, lhs, rhs));
        } else {
            return new ExponentToken(symbolPos, lhs, rhs);
        }
    }
}