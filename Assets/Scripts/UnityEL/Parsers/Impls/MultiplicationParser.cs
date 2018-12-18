using UnityEngine;
using UnityEditor;

public class MultiplicationParser : BinaryTokenParser {
    public override string Name { get { return "multiplication"; } }

    public MultiplicationParser() : 
        base(new SingleCharacterParser('*')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new MultiplicationToken(symbolPos, lhs, rhs));
        } else {
            return new MultiplicationToken(symbolPos, lhs, rhs);
        }
    }
}