using UnityEngine;
using UnityEditor;

public class SubtractionParser : BinaryTokenParser {
    public override string Name { get { return "subtraction"; } }

    public SubtractionParser() : 
        base(new SingleCharacterParser('-')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new SubtractionToken(symbolPos, lhs, rhs));
        } else {
            return new SubtractionToken(symbolPos, lhs, rhs);
        }
    }
}