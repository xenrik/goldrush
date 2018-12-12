using UnityEngine;
using UnityEditor;

public class PropertyAccessParserTest : BinaryParserTest<PropertyAccessParser, PropertyAccessToken> {
    public override string ParserSymbol { get { return "."; } }

    protected override TokenImpl GetLhs(PropertyAccessToken token) {
        return token.Host;
    }

    protected override TokenImpl GetRhs(PropertyAccessToken token) {
        return token.Property;
    }
}