public class NotExistsParser : UnaryTokenParser {
    public override string Name { get { return "notExists"; } }

    public NotExistsParser() : 
        base(new WordTokenParser(true, "not", "exists")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new NotExistsToken(symbolPos, rhs);
    }
}