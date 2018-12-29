using UnityEngine;
using UnityEditor;

public class ConditionalOperatorParser : TokenParser {
    public string Name { get { return "conditionalOperator"; } }

    private SingleCharacterParser testSymbol = new SingleCharacterParser('?');
    private SingleCharacterParser elseSymbol = new SingleCharacterParser(':');

    public bool Parse(ExpressionCompiler compiler) {
        int startPos = compiler.Pos;
        if (!testSymbol.Parse(compiler)) {
            return false;
        }

        // Must have a left-hand side...
        if (compiler.Parent.Children.Count == 0) {
            // Reset the compiler position
            compiler.Pos = startPos;

            throw new ParserException(Name, startPos, "Missing operand for the test");
        }
        TokenImpl test = compiler.Parent.PopChild();

        // Parse the result if true token
        if (!compiler.ParseNextToken()) {
            // Reset the compiler position and restore the removed child
            compiler.Pos = startPos;
            compiler.Parent.AddChild(test);

            throw new ParserException(Name, startPos, "Missing operand for result if true");
        }
        TokenImpl resultIfTrue = compiler.Parent.PopChild();

        // Must have the 'else' token next
        if (!elseSymbol.Parse(compiler)) {
            // Reset the compiler position and restore the (first) removed child
            compiler.Pos = startPos;
            compiler.Parent.AddChild(test);

            throw new ParserException(Name, startPos, "Missing ':' symbol");
        }

        // Parse the result if false token
        if (!compiler.ParseNextToken()) {
            // Reset the compiler position and restore the (first) removed child
            compiler.Pos = startPos;
            compiler.Parent.AddChild(test);

            throw new ParserException(Name, startPos, "Missing operand for result if false");
        }
        TokenImpl resultIfFalse = compiler.Parent.PopChild();

        TokenImpl token = new ConditionalOperatorToken(startPos, test, resultIfTrue, resultIfFalse);
        compiler.Parent.AddChild(token);
        return true;
    }
}