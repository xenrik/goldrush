using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UnityELExpressionCompiler {
    private List<TokenParser> parsers = new List<TokenParser>();

    public UnityELExpressionCompiler() {
        // Special parsers to close groups, functions, or keyed access
        parsers.Add(new ClosePreviousParser()); // ) or ]                           --

        // Function, Property and Identifiers 
        parsers.Add(new PropertyAccessorParser()); // <identifier> followed by .    -- DONE
        parsers.Add(new KeyedAccessorParser()); // <identifier> followed by [       -- DONE
        parsers.Add(new FunctionParser()); // <identifier> followed by (            -- DONE
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                      -- DONE
        parsers.Add(new ArgumentParser()); // ,                                     -- DONE

        // Primitives
        parsers.Add(new StringParser()); // " or '                                  -- DONE
        parsers.Add(new BooleanParser()); // true or false                          -- DONE
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                          -- DONE
        parsers.Add(new IntegerParser()); // [0-9]+                                 -- DONE

        // Maths
        parsers.Add(new GroupParser()); // (                                        -- DONE
        parsers.Add(new AdditionParser()); // +                                     -- DONE
        parsers.Add(new SubtractionParser()); // -                                  -- DONE
        parsers.Add(new DivisionParser()); // /                                     -- DONE
        parsers.Add(new MultiplicationParser()); // *                               -- DONE
        parsers.Add(new ModulusParser()); // %                                      -- DONE
        parsers.Add(new PowerParser()); // <integer> or <decimal> followed by ^     -- DONE

        // Logical
        parsers.Add(new NotParser()); // !                                          --
        parsers.Add(new OrParser()); // ||                                          --
        parsers.Add(new AndParser()); // &&                                         --
        parsers.Add(new XorParser()); // <boolean> followed by ^                    --

        // Bitwise
        parsers.Add(new ComplimentParser()); // ~                                   --
        parsers.Add(new BitwiseAndParser()); // &                                   --
        parsers.Add(new BitwiseOrParser()); // |                                    --
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
