public class AssignParser : BinaryTokenParser {
    public override string Name { get { return "assign"; } }

    public AssignParser() : 
        base(new SingleCharacterParser('=')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new AssignToken(symbolPos, lhs, rhs));
        } else {
            return new AssignToken(symbolPos, lhs, rhs);
        }
    }
}