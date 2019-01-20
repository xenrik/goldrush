public class IncrementAndReturnParser : UnaryTokenParser {
    public override string Name { get { return "incrementAndReturn"; } }

    public IncrementAndReturnParser() : 
        base(new DoubleCharacterParser('+')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs) {
        return new IncrementAndReturnToken(symbolPos, rhs);
    }
}