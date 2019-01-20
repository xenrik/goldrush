public class SubtractAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "subtractAndAssign"; } }

    public SubtractAndAssignParser() : 
        base(new StringTokenParser("-=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new SubtractAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new SubtractAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}