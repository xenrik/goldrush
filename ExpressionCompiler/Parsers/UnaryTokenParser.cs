public abstract class UnaryTokenParser : TokenParser {

    public abstract string Name { get; }

    private TokenParser symbolParser;

    public UnaryTokenParser(TokenParser symbolParser) {
        this.symbolParser = symbolParser;
    }

    public bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!symbolParser.Parse(compiler)) {
            return false;
        }

        // Parse the right hand side
        if (!compiler.ParseNextToken()) {
            // Reset the compiler position 
            compiler.Pos = symbolPos;

            throw new ParserException(Name, symbolPos, "Missing right-hand operand");
        }
        TokenImpl rhs = compiler.Parent.PopChild();

        TokenImpl token = CreateToken(compiler, symbolPos, rhs);
        if (token != null) {
            compiler.Parent.AddChild(token);
        }

        return true;
    }

    protected abstract TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl rhs);
}