using UnityEngine;
using UnityEditor;

public class NotParser : UnaryTokenParser {
    public override string Name { get { return "not"; } }

    public NotParser() : 
        base(new SingleCharacterParser('!')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new NotToken(symbolPos, rhs);
    }
}