using UnityEngine;
using UnityEditor;

public class PropertyAccessParser : BinaryTokenParser {
    public override string Name { get { return "propertyAccess"; } }

    public PropertyAccessParser() : 
        base(new SingleCharacterParser('.')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (!(rhs is IdentifierToken)) {
            throw new ParserException(Name, symbolPos, "Right-hand side of a property access token must be an identifier");
        }

        // The lhs must be an identifier or a property access. If it's not see if we can do anything
        if (lhs is IdentifierToken || lhs is PropertyAccessToken) {
            return new PropertyAccessToken(symbolPos, lhs, (IdentifierToken)rhs);
        } else if (lhs is BinaryToken) {
            // If the RHS of the binary token is an Identifier or Property Access we can join to it
            BinaryToken binaryLhs = (BinaryToken)lhs;
            TokenImpl lhsRhs = binaryLhs.Rhs;

            if (lhsRhs is IdentifierToken || lhsRhs is PropertyAccessToken) {
                // We can join...
                binaryLhs.Rhs = new PropertyAccessToken(symbolPos, lhsRhs, (IdentifierToken)rhs);
                return binaryLhs;
            }
        }

        // Can't work with the lhs
        throw new ParserException(Name, symbolPos, $"Left-hand side of a property access token cannot be: {lhs.Name}");
    }
}

