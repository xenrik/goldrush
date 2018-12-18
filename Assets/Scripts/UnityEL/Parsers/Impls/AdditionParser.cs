using UnityEngine;
using UnityEditor;

public class AdditionParser : BinaryTokenParser {
    public override string Name { get { return "addition"; } }

    public AdditionParser() : 
        base(new SingleCharacterParser('+')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new AdditionToken(symbolPos, lhs, rhs));
        } else {
            return new AdditionToken(symbolPos, lhs, rhs);
        }
    }
}