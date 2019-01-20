public class ExponentAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "exponentAndAssign"; } }

    public ExponentAndAssignParser() : 
        base(new StringTokenParser("**=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ExponentAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new ExponentAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}