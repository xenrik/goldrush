using UnityEngine;
using UnityEditor;

public class ReturnAndIncrementParser : LeftHandUnaryTokenParser {
    public override string Name { get { return "returnAndIncrement"; } }

    public ReturnAndIncrementParser() : 
        base(new DoubleCharacterParser('+')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ReturnAndIncrementToken(symbolPos, lhs));
        } else {
            return new ReturnAndIncrementToken(symbolPos, lhs);
        }
    }
}