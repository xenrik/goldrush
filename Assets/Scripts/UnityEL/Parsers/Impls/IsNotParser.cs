using UnityEngine;
using UnityEditor;

public class IsNotParser : BinaryTokenParser {
    public override string Name { get { return "isNot"; } }

    public IsNotParser() : 
        base(new WordTokenParser(true, "is", "not")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new IsNotToken(symbolPos, lhs, rhs));
        } else {
            return new IsNotToken(symbolPos, lhs, rhs);
        }
    }
}