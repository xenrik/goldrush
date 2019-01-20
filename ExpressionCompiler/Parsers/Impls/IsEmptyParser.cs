public class IsEmptyParser : UnaryTokenParser {
    public override string Name { get { return "empty"; } }

    public IsEmptyParser() : 
        base(new WordTokenParser(true, "empty")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new IsEmptyToken(symbolPos, rhs);
    }
}