public class ComplementParser : UnaryTokenParser {
    public override string Name { get { return "complement"; } }

    public ComplementParser() : 
        base(new SingleCharacterParser('~')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new ComplementToken(symbolPos, rhs);
    }
}