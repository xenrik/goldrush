using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UnityELExpressionCompiler {
    private List<TokenParser> parsers = new List<TokenParser>();

    public UnityELExpressionCompiler() {
        // Special parser to close groups, functions, or keyed access
        parsers.Add(new CloseParser()); // ) or ]                                   -- DONE

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
        parsers.Add(new ExponentParser()); // **                                    -- DONE

        // Logical
        parsers.Add(new NotParser()); // !                                          -- DONE
        parsers.Add(new OrParser()); // ||                                          -- DONE
        parsers.Add(new AndParser()); // &&                                         -- DONE

        // Coalesce and If
        parsers.Add(new NullCoalesceParser()); // ??                                -- DONE
        parsers.Add(new ConditionalOperatorParser()); // ?                          -- DONE

        // Bitwise
        parsers.Add(new ComplementParser()); // ~                                   -- DONE
        parsers.Add(new BitwiseAndParser()); // &                                   -- DONE
        parsers.Add(new BitwiseOrParser()); // |                                    -- DONE
        parsers.Add(new XorParser()); // ^                                          -- DONE
    }

    public UnityELExpression<T> Compile<T>(string expression, UnityELEvaluator context) {
        // First, separate the string into tokens
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
