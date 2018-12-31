using UnityEngine;
using UnityEditor;

public class ShiftLeftParser : BinaryTokenParser {
    public override string Name { get { return "shiftLeft"; } }

    public ShiftLeftParser() : 
        base(new DoubleCharacterParser('<')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ShiftLeftToken(symbolPos, lhs, rhs));
        } else {
            return new ShiftLeftToken(symbolPos, lhs, rhs);
        }
    }
}