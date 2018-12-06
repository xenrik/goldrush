using System.Collections.Generic;
using System;

public class UnityELExpressionCompiler {
    private List<TokenParser> parsers = new List<TokenParser>();

    public UnityELExpressionCompiler() {
        // Special parser to close groups, functions, or keyed access
        parsers.Add(new CloseParser()); // ) or ]                                   --

        // We can't work out just from the current symbol if this is a group or 
        // function, so we have to do that during resolve, rather than parse
        parsers.Add(new GroupOrFunctionParser()); // (                              --

        // Function, Property and Identifiers 
        parsers.Add(new PropertyAccessorParser()); // .                             --
        parsers.Add(new KeyedAccessorParser()); // [                                --
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                      --
        parsers.Add(new ArgumentParser()); // ,                                     --

        // Primitives
        parsers.Add(new StringParser()); // " or '                                  -- DONE
        parsers.Add(new BooleanParser()); // true or false                          -- DONE
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                          -- DONE
        parsers.Add(new IntegerParser()); // [0-9]+                                 -- DONE
        // 0b0101010
        // 0x0123ABC

        // Maths
        parsers.Add(new AdditionParser()); // +                                     -- DONE
        parsers.Add(new SubtractionParser()); // -                                  -- DONE
        parsers.Add(new DivisionParser()); // /                                     -- DONE
        parsers.Add(new MultiplicationParser()); // *                               -- DONE
        parsers.Add(new ModulusParser()); // %                                      -- DONE
        parsers.Add(new ExponentParser()); // **                                    -- DONE

        // Logical
        parsers.Add(new NotParser()); // !                                          --
        parsers.Add(new OrParser()); // ||                                          -- DONE
        parsers.Add(new AndParser()); // &&                                         -- DONE

        // Coalesce and If
        parsers.Add(new NullCoalesceParser()); // ??                                -- DONE
        parsers.Add(new ConditionalOperatorParser()); // ?                          --
        // : (conditional else)

        // Bitwise
        parsers.Add(new ComplementParser()); // ~                                   --
        parsers.Add(new BitwiseAndParser()); // &                                   -- DONE
        parsers.Add(new BitwiseOrParser()); // |                                    -- DONE
        parsers.Add(new XorParser()); // ^                                          -- DONE

        // Comparison
        // <=
        // >=
        // <
        // >
        // ==
        
    }

    public UnityELExpression<T> Compile<T>(string expression, UnityELEvaluator context) {
        // First, separate the string into tokens
        Stack<RawToken> rawTokens = new Stack<RawToken>();
        char[] chars = expression.ToCharArray();
        int pos = 0;
        while (pos < chars.Length) {
            foreach (TokenParser parser in parsers) {
                RawToken token = parser.Consume(chars, ref pos);
                if (token != null) {
                    rawTokens.Push(token);
                }
            }
        }

        // Now go through each token in turn and ask it to resolve itself. 
        Stack<Token> resolvedTokens = new Stack<Token>();
        while (rawTokens.Count > 0) {
            RawToken rawToken = rawTokens.Pop();
            Token resolvedToken = rawToken.Resolve(rawTokens, resolvedTokens);
            if (resolvedToken != null) {
                resolvedTokens.Push(resolvedToken);
            }
        }

        return null;
    }
}
