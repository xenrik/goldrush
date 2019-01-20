public class IsNotEmptyParser : UnaryTokenParser {
    public override string Name { get { return "notEmpty"; } }

    public IsNotEmptyParser() : 
        base(new WordTokenParser(true, "not", "empty")) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new IsNotEmptyToken(symbolPos, rhs);
    }
}