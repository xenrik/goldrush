using UnityEngine;
using UnityEditor;

public class PropertyAccessParser : BinaryTokenParser {
    public override string Name { get { return "propertyAccess"; } }

    public PropertyAccessParser() : 
        base(new SingleCharacterParser('.')) {
    }

    protected override TokenImpl CreateToken(int symbolPos, TokenImpl lhs, TokenImpl rhs) {
        return new PropertyAccessToken(symbolPos, lhs, rhs);
    }
}

