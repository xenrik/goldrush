public class OrParser : BinaryTokenParser {
    public override string Name { get { return "or"; } }

    public OrParser() : 
        base(new DoubleCharacterParser('|')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new OrToken(symbolPos, lhs, rhs));
        } else {
            return new OrToken(symbolPos, lhs, rhs);
        }
    }
}