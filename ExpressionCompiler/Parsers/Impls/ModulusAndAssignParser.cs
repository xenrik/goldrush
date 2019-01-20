public class ModulusAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "modulusAndAssign"; } }

    public ModulusAndAssignParser() : 
        base(new StringTokenParser("%=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new ModulusAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new ModulusAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}