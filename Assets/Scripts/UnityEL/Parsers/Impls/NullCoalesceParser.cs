using UnityEngine;
using UnityEditor;

public class NullCoalesceParser : BinaryTokenParser {
    public override string Name { get { return "nullCoalesce"; } }

    public NullCoalesceParser() : 
        base(new DoubleCharacterParser('?')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new NullCoalesceToken(symbolPos, lhs, rhs));
        } else {
            return new NullCoalesceToken(symbolPos, lhs, rhs);
        }
    }
}