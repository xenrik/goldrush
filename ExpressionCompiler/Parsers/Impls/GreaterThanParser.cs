public class GreaterThanParser : BinaryTokenParser {
    public override string Name { get { return "greaterThan"; } }

    public GreaterThanParser() : 
        base(new SingleCharacterParser('>')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new GreaterThanToken(symbolPos, lhs, rhs));
        } else {
            return new GreaterThanToken(symbolPos, lhs, rhs);
        }
    }
}