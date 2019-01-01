using UnityEngine;
using UnityEditor;

public class AsParser : BinaryTokenParser {
    public override string Name { get { return "as"; } }

    public AsParser() : 
        base(new WordTokenParser(true, "as")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new AsToken(symbolPos, lhs, rhs));
        } else {
            return new AsToken(symbolPos, lhs, rhs);
        }
    }
}