using UnityEngine;
using UnityEditor;

public class DecrementAndReturnParser : UnaryTokenParser {
    public override string Name { get { return "decrementAndReturn"; } }

    public DecrementAndReturnParser() : 
        base(new DoubleCharacterParser('-')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new DecrementAndReturnToken(symbolPos, rhs);
    }
}