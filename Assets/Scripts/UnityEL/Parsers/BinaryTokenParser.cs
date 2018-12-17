using UnityEngine;
using UnityEditor;

public abstract class BinaryTokenParser : TokenParser {

    public abstract string Name { get; }

    private TokenParser symbolParser;

    public BinaryTokenParser(TokenParser symbolParser) {
        this.symbolParser = symbolParser;
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

            throw new ParserException(Name, symbolPos, "Missing left-hand operand");
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

        TokenImpl token = CreateToken(symbolPos, lhs, rhs);
        if (token != null) {
            compiler.Parent.AddChild(token);
        }

        return true;
    }

    protected abstract TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs);
}