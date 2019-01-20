public class BitwiseOrParser : BinaryTokenParser {
    public override string Name { get { return "bitwiseOr"; } }

    public BitwiseOrParser() : 
        base(new SingleCharacterParser('|')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new BitwiseOrToken(symbolPos, lhs, rhs));
        } else {
            return new BitwiseOrToken(symbolPos, lhs, rhs);
        }
    }
}