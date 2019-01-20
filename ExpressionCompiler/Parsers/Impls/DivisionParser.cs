public class DivisionParser : BinaryTokenParser {
    public override string Name { get { return "division"; } }

    public DivisionParser() : 
        base(new SingleCharacterParser('/')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new DivisionToken(symbolPos, lhs, rhs));
        } else {
            return new DivisionToken(symbolPos, lhs, rhs);
        }
    }
}