public class BitwiseAndParser : BinaryTokenParser {
    public override string Name { get { return "bitwiseAnd"; } }

    public BitwiseAndParser() : 
        base(new SingleCharacterParser('&')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (lhs is BinaryToken) {
            return ApplyPrecedence(compiler, (BinaryToken)lhs, new BitwiseAndToken(symbolPos, lhs, rhs));
        } else {
            return new BitwiseAndToken(symbolPos, lhs, rhs);
        }
    }
}