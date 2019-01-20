public class MultiplyAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "multiplyAndAssign"; } }

    public MultiplyAndAssignParser() : 
        base(new StringTokenParser("*=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new MultiplyAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new MultiplyAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}