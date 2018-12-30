using UnityEngine;
using UnityEditor;

public class IsNotEmptyParserTest : UnaryParserTest<IsNotEmptyParser, IsNotEmptyToken> {
    public override string ParserSymbol { get { return "not empty"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}