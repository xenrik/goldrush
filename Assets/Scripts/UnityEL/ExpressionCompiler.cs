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
        parsers.Add(new CloseParser(')')); //                                       
        parsers.Add(new CloseParser(']')); //                                       

        // ********* Tokens which take words

        // Boolean and Null
        parsers.Add(new BooleanParser()); // true or false                             
        parsers.Add(new NullParser()); // null                                      

        // Tests
        parsers.Add(new IsNotParser()); // is not (not instance of)                  
        parsers.Add(new IsParser()); // is (instance of)                                                
        parsers.Add(new IsNotEmptyParser()); // not empty                           
        parsers.Add(new IsEmptyParser()); // empty                                  
        parsers.Add(new NotExistsParser()); // not exists                           
        parsers.Add(new ExistsParser()); // exists                                  

        // Cast
        parsers.Add(new AsParser()); // as (cast)

        // ********* Tokens which take 3 characters

        // Maths
        parsers.Add(new ExponentAndAssignParser()); // **=                          

        // ********* Tokens which take 2 characters

        // Maths
        parsers.Add(new ExponentParser()); // **                                    

        // Bitwise 
        parsers.Add(new ShiftLeftParser()); // <<                                   
        parsers.Add(new ShiftRightParser()); // >>                                  

        // Coalesce
        parsers.Add(new NullCoalesceParser()); // ??                                

        // Tests
        parsers.Add(new ConditionalOperatorParser()); // ?:                                                
        parsers.Add(new LessThanOrEqualsParser()); // <=                            
        parsers.Add(new GreaterThanOrEqualsParser()); // >=                         
        parsers.Add(new EqualsParser()); // ==                                      
        parsers.Add(new NotEqualsParser()); // !=                                   

        // Logical
        parsers.Add(new OrParser()); // ||                                          
        parsers.Add(new AndParser()); // &&                                         

        // Assignment
        parsers.Add(new AddAndAssignParser()); // +=                                
        parsers.Add(new SubtractAndAssignParser()); // -=                           
        parsers.Add(new MultiplyAndAssignParser()); // *=                           
        parsers.Add(new DivideAndAssignParser()); // /=                             
        parsers.Add(new ModulusAndAssignParser()); // %=                            
        parsers.Add(new ReturnAndIncrementParser()); // <identifier>++              
        parsers.Add(new ReturnAndDecrementParser()); // <identifier>--              
        parsers.Add(new IncrementAndReturnParser()); // ++<identifier>              
        parsers.Add(new DecrementAndReturnParser()); // --<identifier>              

        // ********* Tokens which take single characters

        // Function and Group
        parsers.Add(new FunctionParser()); // <identifier>(                         
        parsers.Add(new GroupParser()); // (                                                             

        // Should probably move into the function parser
        parsers.Add(new SingleCharacterParser(',')); // argument separator -- we don't need a token for this

        // Property Access
        parsers.Add(new PropertyAccessParser()); // .                               
        parsers.Add(new KeyedAccessParser()); // [                                                       

        // Primitives 
        parsers.Add(new StringParser()); // " or '                                               
        parsers.Add(new BinaryIntegerParser()); // 0b0101010                        
        parsers.Add(new HexIntegerParser()); // 0x0123ABC                           
        parsers.Add(new DecimalParser()); // [0-9]+.[0-9]+                          
        parsers.Add(new IntegerParser()); // [0-9]+                                 

        // Maths
        parsers.Add(new MultiplicationParser()); // *                               
        parsers.Add(new AdditionParser()); // +                                     
        parsers.Add(new SubtractionParser()); // -                                                               
        parsers.Add(new UnaryMinusParser()); // -                                   
        parsers.Add(new DivisionParser()); // /                                     
        parsers.Add(new ModulusParser()); // %                                      

        // Tests
        parsers.Add(new LessThanParser()); // <                                     
        parsers.Add(new GreaterThanParser()); // >                                  

        // Logical
        parsers.Add(new NotParser()); // !                                          

        // Bitwise (single characters)
        parsers.Add(new ComplementParser()); // ~                                                               
        parsers.Add(new BitwiseAndParser()); // &                                   
        parsers.Add(new BitwiseOrParser()); // |                                    
        parsers.Add(new XorParser()); // ^                                               

        // Assignment
        parsers.Add(new AssignParser()); // =                                       

        // Identifier (should be last to avoid collision with other parsers)
        parsers.Add(new IdentifierParser()); // [a-Z][a-z0-9]+                      
    }

    /**
     * Given a Token type, return its precedence. Returns int.MaxValue if the precedence is
     * undefined. Numbers are based on https://en.wikipedia.org/wiki/Order_of_operations
     */
    public int GetPrecedence(Type type) {
        // Left Hand Unary Tokens (<identifier>++ and <identifer>--) have the highest
        // precedence
        if (type == typeof(LeftHandUnaryToken)) {
            return 0;
        } else if (type == typeof(FunctionToken) ||
            type == typeof(PropertyAccessParser) ||
            type == typeof(GroupToken)) {
            return 10;
        } else if (type == typeof(MultiplicationToken) ||
            type == typeof(DivisionToken)) {
            return 30;
        } else if (type == typeof(AdditionToken) ||
            type == typeof(SubtractionToken)) {
            return 40;
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
