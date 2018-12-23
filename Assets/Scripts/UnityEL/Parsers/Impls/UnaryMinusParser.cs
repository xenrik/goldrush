using UnityEngine;
using UnityEditor;

public class UnaryMinusParser : UnaryTokenParser {
    public override string Name { get { return "unaryMinus"; } }

    public UnaryMinusParser() : 
        base(new SingleCharacterParser('-')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new UnaryMinusToken(symbolPos, rhs);
    }
}