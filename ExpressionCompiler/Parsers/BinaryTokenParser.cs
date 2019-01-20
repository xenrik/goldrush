public abstract class BinaryTokenParser : TokenParser {

    public abstract string Name { get; }

    private TokenParser symbolParser;
    private bool lhsLenient;

    public BinaryTokenParser(TokenParser symbolParser, bool lhsLenient = false) {
        this.symbolParser = symbolParser;
        this.lhsLenient = lhsLenient;
    }

    public bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!symbolParser.Parse(compiler)) {
            return false;
        }

        // Must have a left-hand side...
        if (compiler.Parent.Children.Count == 0) {
            // Reset the compiler position
            compiler.Pos = symbolPos;

            // If we are lenient to the left-hand side missing, just return false
            if (lhsLenient) {
                return false;
            } else {
                throw new ParserException(Name, symbolPos, "Missing left-hand operand");
            }
        }
        TokenImpl lhs = compiler.Parent.PopChild();

        // Parse the right hand side
        if (!compiler.ParseNextToken()) {
            // Reset the compiler position and restore the removed child
            compiler.Pos = symbolPos;
            compiler.Parent.AddChild(lhs);

            throw new ParserException(Name, symbolPos, "Missing right-hand operand");
        }
        TokenImpl rhs = compiler.Parent.PopChild();

        TokenImpl token = CreateToken(compiler, symbolPos, lhs, rhs);
        if (token != null) {
            compiler.Parent.AddChild(token);
        }

        return true;
    }

    protected TokenImpl ApplyPrecedence(ExpressionCompiler compiler, BinaryToken lhs, BinaryToken newToken) {
        // If the left-hand side is a binary token, and the new token has a higher precedence, 
        // then take the right-hand side from the old left-hand side and use that as the left
        // hand side on the new token. The new token then becomes the right-hand side of the left
        // hand side.
        int lhsPrecedence = compiler.GetPrecedence(lhs.GetType());
        int newPrecedence = compiler.GetPrecedence(newToken.GetType());

        if (newPrecedence < lhsPrecedence) {
            newToken.Lhs = lhs.Rhs;
            lhs.Rhs = newToken;

            return lhs;
        } else {
            return newToken;
        }
    }

    protected abstract TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs);
}