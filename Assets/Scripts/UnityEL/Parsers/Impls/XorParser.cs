using UnityEngine;
using UnityEditor;

public class XorParser : BinaryTokenParser {
    public override string Name { get { return "xor"; } }

    public XorParser() : 
        base(new SingleCharacterParser('^')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new XorToken(symbolPos, lhs, rhs));
        } else {
            return new XorToken(symbolPos, lhs, rhs);
        }
    }
}