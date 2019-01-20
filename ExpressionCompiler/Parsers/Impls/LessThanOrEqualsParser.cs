public class LessThanOrEqualsParser : BinaryTokenParser {
    public override string Name { get { return "lessThanOrEquals"; } }

    public LessThanOrEqualsParser() : 
        base(new StringTokenParser("<=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new LessThanOrEqualsToken(symbolPos, lhs, rhs));
        } else {
            return new LessThanOrEqualsToken(symbolPos, lhs, rhs);
        }
    }
}