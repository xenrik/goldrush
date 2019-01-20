public class FunctionParser : SingleCharacterParser {
    public FunctionParser() : base('(') {
    }

    public override bool Parse(ExpressionCompiler compiler) {
        int symbolPos = compiler.Pos;
        if (!base.Parse(compiler)) {
            return false;
        }

        // Must have a left-hand side...
        if (compiler.Parent.Children.Count == 0) {
            // Reset the compiler position
            compiler.Pos = symbolPos;

            // We don't throw an exception so the compiler will then check to see
            // if the symbol represents a group instead
            return false;
        }

        TokenImpl lhs = compiler.Parent.PopChild();
        FunctionToken function = null;

        if (lhs is HostSupport) {
            function = new FunctionToken(symbolPos, lhs);
            compiler.Parent.AddChild(function);
        } else if (lhs is BinaryToken) {
            // If the RHS of the binary token is token which supports hosting we can join to and replace it
            BinaryToken binaryLhs = (BinaryToken)lhs;
            TokenImpl lhsRhs = binaryLhs.Rhs;

            if (lhsRhs is HostSupport) {
                // We can join it
                function = new FunctionToken(symbolPos, lhsRhs);
                binaryLhs.Rhs = function;

                compiler.Parent.AddChild(binaryLhs);
            }
        } else if (lhs is UnaryToken) {
            // If the RHS of the unary token is token which supports hosting we can join to and replace it
            UnaryToken unaryLhs = (UnaryToken)lhs;
            TokenImpl lhsRhs = unaryLhs.Rhs;

            if (lhsRhs is HostSupport) {
                // We can join it
                function = new FunctionToken(symbolPos, lhsRhs);
                unaryLhs.Rhs = function;

                compiler.Parent.AddChild(unaryLhs);
            }
        }
        
        if (function == null) {
            // Restore the lhs and position
            compiler.Parent.AddChild(lhs);
            compiler.Pos = symbolPos;

            // Couldn't handle the lhs, just return false to continue to parse as a group
            return false;
        }

        // The function becomes the new parent token
        compiler.ParentTokens.Push(function);

        // Keep parsing until we are closed
        while (!function.IsClosed) {
            if (!compiler.ParseNextToken()) {
                throw new ParserException(function, "Unclosed function");
            }
        }

        return true;
    }
}