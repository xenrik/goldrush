using System.Collections.Generic;
using System;

public class ExpressionCompiler {
    private List<TokenParser> parsers = new List<TokenParser>();

    public char[] Expression { get; private set; }
    public int Pos { get; set; }

    public Stack<TokenImpl> ParentTokens { get; private set; }

    // Convenience method to get the current parent
    public TokenImpl Parent {
        get {
            return ParentTokens.Peek();
        }
    }

    public ExpressionCompiler(string expression) {
        initParsers();

        Expression = expression.ToCharArray();
        ParentTokens = new Stack<TokenImpl>();
    }

    private void initParsers() {
        // These must be added in the order that parsers are to be selected. 

        // Special parser to close groups, functions, or keyed access (these should probably move
        // into the relevant parser now)
        parsers.Add(new CloseParser(')')); //                                       -- Done
        parsers.Add(new CloseParser(']')); //                                       -- Done

        // ********* Tokens which take words

        // Boolean and Null
        parsers.Add(new BooleanParser()); // true or false                          -- Done   
        parsers.Add(new NullParser()); // null                                      -- Done

        // Tests
        parsers.Add(new IsNotParser()); // is not (not instance of)                 -- Done 
        parsers.Add(new IsParser()); // is (instance of)                            -- Done                    
        parsers.Add(new IsNotEmptyParser()); // not empty                           -- Done
        parsers.Add(new IsEmptyParser()); // empty                                  -- Done
        parsers.Add(new NotExistsParser()); // not exists                           -- Done
        parsers.Add(new ExistsParser()); // exists                                  -- Done

        // ********* Tokens which take 3 characters

        // Maths
        parsers.Add(new ExponentAndAssignParser()); // **=                          -- Done

        // ********* Tokens which take 2 characters

        // Maths
        parsers.Add(new ExponentParser()); // **                                    -- Done

        // Bitwise 
        parsers.Add(new ShiftLeftParser()); // <<                                   -- Done
        parsers.Add(new ShiftRightParser()); // >>                                  -- Done

        // Coalesce
        parsers.Add(new NullCoalesceParser()); // ??                                -- Done

        // Tests
        parsers.Add(new ConditionalOperatorParser()); // ?:                         -- Done                       
        parsers.Add(new LessThanOrEqualsParser()); // <=                            -- Done
        parsers.Add(new GreaterThanOrEqualsParser()); // >=                         -- Done
        parsers.Add(new EqualsParser()); // ==                                      -- Done
        parsers.Add(new NotEqualsParser()); // !=                                   -- Done

        // Logical
        parsers.Add(new OrParser()); // ||                                          -- Done
        parsers.Add(new AndParser()); // &&                                         -- Done

        // Assignment
        parsers.Add(new AddAndAssignParser()); // +=                                -- Done
        parsers.Add(new SubtractAndAssignParser()); // -=                           -- Done
        parsers.Add(new MultiplyAndAssignParser()); // *=                           -- Done
        parsers.Add(new DivideAndAssignParser()); // /=                             -- Done
        parsers.Add(new ModulusAndAssignParser()); // %=                            -- Done
        // parsers.Add(new IncrementAndEvaluateParser()); // ++<identifier>
        // parsers.Add(new DecrementAndEvaluateParser()); // --<identifier>
        // parsers.Add(new EvaluateAndIncrementParser()); // <identifier>++
        // parsers.Add(new EvaluateAndDecrementParser()); // <identifier>--

        // ********* Tokens which take single characters

        // Function and Group
        parsers.Add(new FunctionParser()); // <identifier>(                         -- Done
        parsers.Add(new GroupParser()); // (                                        -- Done                     

        // Should probably move into the function parser
        parsers.Add(new SingleCharacterParser(',')); // argument separator -- we don't need a token for this

        // Property Access
        parsers.Add(new PropertyAccessParser()); // .                               -- Done
        parsers.Add(new KeyedAccessParser()); // [                                  -- Done                     

        // Primitives 
        parsers.Add(new StringParser()); // " or '                                  -- Done             
        parsers.Add(new BinaryIntegerParser()); // 0b0101010                        -- Done
        parsers.Add(new HexIntegerParser()); // 0x0123ABC                           -- Done
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                          -- Done
        parsers.Add(new IntegerParser()); // [0-9]+                                 -- Done

        // Maths
        parsers.Add(new MultiplicationParser()); // *                               -- Done
        parsers.Add(new AdditionParser()); // +                                     -- Done
        parsers.Add(new SubtractionParser()); // -                                  -- Done                             
        parsers.Add(new UnaryMinusParser()); // -                                   -- Done
        parsers.Add(new DivisionParser()); // /                                     -- Done
        parsers.Add(new ModulusParser()); // %                                      -- Done

        // Tests
        parsers.Add(new LessThanParser()); // <                                     -- Done
        parsers.Add(new GreaterThanParser()); // >                                  -- Done

        // Logical
        parsers.Add(new NotParser()); // !                                          -- Done

        // Bitwise (single characters)
        parsers.Add(new ComplementParser()); // ~                                   -- Done                            
        parsers.Add(new BitwiseAndParser()); // &                                   -- Done
        parsers.Add(new BitwiseOrParser()); // |                                    -- Done
        parsers.Add(new XorParser()); // ^                                          -- Done     

        // Assignment
        parsers.Add(new AssignParser()); // =                                       -- Done

        // Other
        // parsers.Add(new AsParser()); // as (cast)

        // Identifier (should be last to avoid collision with other parsers)
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                      -- Done
    }

    /**
     * Given a Token type, return its precedence. Returns int.MaxValue if the precedence is
     * undefined. Numbers are shameless stolen from https://en.wikipedia.org/wiki/Order_of_operations
     */
    public int GetPrecedence(Type type) {
        if (type == typeof(FunctionToken) ||
            type == typeof(PropertyAccessParser) ||
            type == typeof(GroupToken)) {
            return 1;
        } else if (type == typeof(MultiplicationToken) ||
            type == typeof(DivisionToken)) {
            return 3;
        } else if (type == typeof(AdditionToken) ||
            type == typeof(SubtractionToken)) {
            return 4;
        } else {
            return int.MaxValue;
        }
    }

    public UnityELExpression<T> Compile<T>() {
        RootToken rootToken = new RootToken();
        ParentTokens.Clear();
        ParentTokens.Push(rootToken);

        while (Pos < Expression.Length) {
            if (!ParseNextToken()) {
                throw new ParserException(Pos, "Unknown token found");
            }
        }

        if (Parent != rootToken) {
            throw new ParserException(Pos, "Incomplete expression");
        }
        rootToken.Validate();

        return new ExpressionImpl<T>(rootToken);
    }

    /**
     * Parse a token from the char array, starting at the given position. The token
     * should be a
     */
    public bool ParseNextToken() {
        foreach (TokenParser parser in parsers) {
            if (parser.Parse(this)) {
                return true;
            }
        }

        return false;
    }
}
