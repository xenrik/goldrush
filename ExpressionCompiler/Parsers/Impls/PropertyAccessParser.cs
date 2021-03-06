﻿public class PropertyAccessParser : BinaryTokenParser {
    public override string Name { get { return "propertyAccess"; } }

    public PropertyAccessParser() : 
        base(new SingleCharacterParser('.')) {
    }

    protected override TokenImpl CreateToken(ExpressionCompiler compiler, int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (!(rhs is IdentifierToken)) {
            throw new ParserException(Name, symbolPos, "Right-hand side of a property access token must be an identifier");
        }

        // The lhs must be a token which supports Hosting. If it's not see if we can do anything
        if (lhs is HostSupport) {
            return new PropertyAccessToken(symbolPos, lhs, (IdentifierToken)rhs);
        } else if (lhs is BinaryToken) {
            // If the RHS of the binary token is a token which allows hosting we can join to it
            BinaryToken binaryLhs = (BinaryToken)lhs;
            TokenImpl lhsRhs = binaryLhs.Rhs;

            if (lhsRhs is HostSupport) {
                // We can join...
                binaryLhs.Rhs = new PropertyAccessToken(symbolPos, lhsRhs, (IdentifierToken)rhs);
                return binaryLhs;
            }
        } else if (lhs is UnaryToken) {
            // If the RHS of the unary token is a token which allows hosting we can join to and replace it
            UnaryToken unaryLhs = (UnaryToken)lhs;
            TokenImpl lhsRhs = unaryLhs.Rhs;

            if (lhsRhs is HostSupport) {
                // We can join it
                unaryLhs.Rhs = new PropertyAccessToken(symbolPos, lhsRhs, (IdentifierToken)rhs);
                return unaryLhs;
            }
        }

        // Can't work with the lhs
        throw new ParserException(Name, symbolPos, $"Left-hand side of a property access token cannot be: {lhs.Name}");
    }
}

