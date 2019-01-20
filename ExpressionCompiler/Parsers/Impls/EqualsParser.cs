public class EqualsParser : BinaryTokenParser {
    public override string Name { get { return "Equals"; } }

    public EqualsParser() : 
        base(new DoubleCharacterParser('=')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new EqualsToken(symbolPos, lhs, rhs));
        } else {
            return new EqualsToken(symbolPos, lhs, rhs);
        }
    }
}