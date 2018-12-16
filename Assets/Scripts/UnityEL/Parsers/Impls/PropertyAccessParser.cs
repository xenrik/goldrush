using UnityEngine;
using UnityEditor;

public class PropertyAccessParser : BinaryTokenParser {
    public override string Name { get { return "propertyAccess"; } }

    public PropertyAccessParser() : 
        base(new SingleCharacterParser('.')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        if (!(rhs is IdentifierToken)) {
            throw new ParserException("propertyAccess", symbolPos, "Right-hand side of a property access token must be an identifier");
        }

        return new PropertyAccessToken(symbolPos, lhs, (IdentifierToken)rhs);
    }
}

