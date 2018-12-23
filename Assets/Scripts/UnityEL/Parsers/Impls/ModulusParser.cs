using UnityEngine;
using UnityEditor;

public class ModulusParser : BinaryTokenParser {
    public override string Name { get { return "modulus"; } }

    public ModulusParser() : 
        base(new SingleCharacterParser('%')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ModulusToken(symbolPos, lhs, rhs));
        } else {
            return new ModulusToken(symbolPos, lhs, rhs);
        }
    }
}