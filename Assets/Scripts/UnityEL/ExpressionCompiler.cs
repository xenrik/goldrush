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
        // Special parser to close groups, functions, or keyed access
        // parsers.Add(new CloseParser()); // ) or ]                                  

        // Function and Group
        // parsers.Add(new FunctionParser()); // <identifier>(                             
        parsers.Add(new GroupParser()); // (                             
        // parsers.Add(new ArgumentParser()); // ,                                    

        // Function, Property and Identifiers 
        parsers.Add(new PropertyAccessParser()); // .                            
        // parsers.Add(new KeyedAccessorParser()); // [                               
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                     

        // Primitives
        parsers.Add(new StringParser()); // " or '                                 
        parsers.Add(new BooleanParser()); // true or false                         
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                         
        parsers.Add(new IntegerParser()); // [0-9]+                                
        // parsers.Add(new BinaryIntegerParser()); // 0b0101010
        // parsers.Add(new HexIntegerParser()); // 0x0123ABC

        // Maths
        parsers.Add(new AdditionParser()); // +                                    
        parsers.Add(new SubtractionParser()); // -                                 
        parsers.Add(new DivisionParser()); // /                                    
        parsers.Add(new MultiplicationParser()); // *                              
        // parsers.Add(new ModulusParser()); // %                                     
        // parsers.Add(new ExponentParser()); // **                                   

        // Logical
        // parsers.Add(new NotParser()); // !                                         
        // parsers.Add(new OrParser()); // ||                                         
        // parsers.Add(new AndParser()); // &&                                        

        // Coalesce and Tests
        // parsers.Add(new NullCoalesceParser()); // ??                               
        // parsers.Add(new ConditionalOperatorParser()); // ?                         
        // parsers.Add(new ConditionalElseParser()); // : (conditional else)          
        // parsers.Add(new IsParser()); // is (instance of)
        // parsers.Add(new LessThanOrEqualToParser()); // <=
        // parsers.Add(new GreterThanOrEqualToParser()); // >=
        // parsers.Add(new LessThanParser()); // <
        // parsers.Add(new GreaterThanParser()); // >
        // parsers.Add(new EqualsParser()); // ==

        // Bitwise
        // parsers.Add(new ComplementParser()); // ~                                  
        // parsers.Add(new BitwiseAndParser()); // &                                  
        // parsers.Add(new BitwiseOrParser()); // |                                   
        // parsers.Add(new XorParser()); // ^                                         

        // Assignment
        // parsers.Add(new AssignParser()); // =
    }

    public UnityELExpression<T> Compile<T>() {
        RootToken rootToken = new RootToken();
        while (Pos < Expression.Length) {
            if (!ParseNextToken()) {
                throw new ParserException(Pos, "Unknown token found");
            }
        }

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
