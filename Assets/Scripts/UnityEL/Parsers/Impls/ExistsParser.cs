using UnityEngine;
using UnityEditor;

public class ExistsParser : UnaryTokenParser {
    public override string Name { get { return "exists"; } }

    public ExistsParser() : 
        base(new WordTokenParser(true, "exists")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new ExistsToken(symbolPos, rhs);
    }
}