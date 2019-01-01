using UnityEngine;
using UnityEditor;

public class ReturnAndDecrementParser : LeftHandUnaryTokenParser {
    public override string Name { get { return "returnAndDecrement"; } }

    public ReturnAndDecrementParser() : 
        base(new DoubleCharacterParser('-')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ReturnAndDecrementToken(symbolPos, lhs));
        } else {
            return new ReturnAndDecrementToken(symbolPos, lhs);
        }
    }
}