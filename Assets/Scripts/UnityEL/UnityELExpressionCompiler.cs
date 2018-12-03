using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UnityELExpressionCompiler {
    private List<TokenParser> parsers = new List<TokenParser>();

    public UnityELExpressionCompiler() {
        // Number, Identifier and String
        parsers.Add(new StringParser()); // " or '
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+
        parsers.Add(new NumberParser()); // [0-9]+

        // Property and Functions
        parsers.Add(new PropertyAccessorParser()); // <identifier>[ or <identifier>.
        parsers.Add(new FunctionParser()); // <identifier>(
        parsers.Add(new GroupParser()); // (

        // Maths
        parsers.Add(new AdditionParser()); // +
        parsers.Add(new SubtractionParser()); // -
        parsers.Add(new DivisionParser()); // /
        parsers.Add(new MultiplicationParser()); // *
        parsers.Add(new ModulusParser()); // %
        parsers.Add(new PowerPaser()); // ^

        // Logical
        parsers.Add(new NotParser()); // !
        parsers.Add(new OrParser()); // ||
        parsers.Add(new AndParser()); // &&

        // Bitwise
        parsers.Add(new ComplimentParser()); // ~
        parsers.Add(new BitwiseAndParser()); // &
        parsers.Add(new BitwiseOrParser()); // |
    }

    public UnityELExpression<T> Compile<T>(string expression, UnityELEvaluator context) {
        Stack<Token> tokens = new Stack<Token>();
        char[] chars = expression.ToCharArray();
        int pos = 0;
        while (pos < chars.Length) {
            foreach (TokenParser parser in parsers) {
                Token token = parser.Consume(tokens, chars, ref pos);
                if (token != null) {
                    tokens.Push(token);
                }
            }
        }

        return null;
    }
}
