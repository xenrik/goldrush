public interface TokenParser {
    /**
     * If possible, parse a token from the expression on the compiler. Return true
     * if a token was parsed.
     */
    bool Parse(ExpressionCompiler compiler);
}
