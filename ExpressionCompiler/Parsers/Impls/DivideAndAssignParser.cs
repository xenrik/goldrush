public class DivideAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "divideAndAssign"; } }

    public DivideAndAssignParser() : 
        base(new StringTokenParser("/=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new DivideAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new DivideAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}