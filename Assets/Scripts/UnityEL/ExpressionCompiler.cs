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

        // Special parser to close groups, functions, or keyed access
        parsers.Add(new CloseParser(')')); //                                       -- Done
        parsers.Add(new CloseParser(']')); //                                       -- Done

        // Function and Group
        parsers.Add(new FunctionParser()); // <identifier>(                         -- Done
        parsers.Add(new GroupParser()); // (                                        -- Done                     
        parsers.Add(new SingleCharacterParser(',')); // argument separator -- we don't need a token for this

        // Function, Property and Identifiers 
        parsers.Add(new PropertyAccessParser()); // .                               -- Done
        // parsers.Add(new KeyedAccessorParser()); // [                               
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                      -- Done

        // Primitives
        parsers.Add(new StringParser()); // " or '                                  -- Done                 
        parsers.Add(new BooleanParser()); // true or false                          -- Done            
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                          -- Done
        parsers.Add(new IntegerParser()); // [0-9]+                                 -- Done
        // parsers.Add(new BinaryIntegerParser()); // 0b0101010
        // parsers.Add(new HexIntegerParser()); // 0x0123ABC

        // Maths
        parsers.Add(new AdditionParser()); // +                                     -- Done
        parsers.Add(new SubtractionParser()); // -                                  -- Done                             
        parsers.Add(new DivisionParser()); // /                                     -- Done
        parsers.Add(new MultiplicationParser()); // *                               -- Done
        // parsers.Add(new ModulusParser()); // %                                     
        // parsers.Add(new ExponentParser()); // **     
        // parsers.Add(new UnaryMinusParser()); // -    

        // Logical
        // parsers.Add(new NotParser()); // !                                         
        // parsers.Add(new OrParser()); // ||                                         
        // parsers.Add(new AndParser()); // &&                                        

        // Coalesce and Tests
        // parsers.Add(new NullCoalesceParser()); // ??                               
        // parsers.Add(new ConditionalOperatorParser()); // ?                         
        // parsers.Add(new ConditionalElseParser()); // : (conditional else)          
        // parsers.Add(new IsParser()); // is (instance of)
        // parsers.Add(new IsEmptyParser()); // empty
        // parsers.Add(new NotEmptyParser()); // notEmpty
        // parsers.Add(new ExistsParser()); // exists
        // parsers.Add(new LessThanOrEqualToParser()); // <=
        // parsers.Add(new GreterThanOrEqualToParser()); // >=
        // parsers.Add(new LessThanParser()); // <
        // parsers.Add(new GreaterThanParser()); // >
        // parsers.Add(new EqualsParser()); // ==
        // parsers.Add(new NotEqualsParser()); // !=

        // Bitwise
        // parsers.Add(new ComplementParser()); // ~                                  
        // parsers.Add(new BitwiseAndParser()); // &                                  
        // parsers.Add(new BitwiseOrParser()); // |                                   
        // parsers.Add(new XorParser()); // ^                                  
        // parsers.Add(new ShiftLeftParser()); // <<
        // parsers.Add(new ShiftRightParser()); // >>                                               

        // Assignment
        // parsers.Add(new AssignParser()); // =
        // parsers.Add(new IncrementAndAssignParser()); // +=
        // parsers.Add(new DecrementAndAssignParser()); // -=
        // parsers.Add(new MultiplyAndAssignParser()); // *=
        // parsers.Add(new DivideAndAssignParser()); // /=
        // parsers.Add(new ModulusAndAssignParser()); // %=
        // parsers.Add(new IncrementAndAssignParser()); // += 
        // parsers.Add(new IncrementParser()); // ++
        // parsers.Add(new DecrementParser()); // --

        // Other
        // parsers.Add(new AsParser()); // as (cast)
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
