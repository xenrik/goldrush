using UnityEngine;
using UnityEditor;

public class AddAndAssignParser : BinaryTokenParser {
    public override string Name { get { return "addAndAssign"; } }

    public AddAndAssignParser() : 
        base(new StringTokenParser("+=")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new AddAndAssignToken(symbolPos, lhs, rhs));
        } else {
            return new AddAndAssignToken(symbolPos, lhs, rhs);
        }
    }
}