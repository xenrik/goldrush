public class LessThanParser : BinaryTokenParser {
    public override string Name { get { return "lessThan"; } }

    public LessThanParser() : 
        base(new SingleCharacterParser('<')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new LessThanToken(symbolPos, lhs, rhs));
        } else {
            return new LessThanToken(symbolPos, lhs, rhs);
        }
    }
}