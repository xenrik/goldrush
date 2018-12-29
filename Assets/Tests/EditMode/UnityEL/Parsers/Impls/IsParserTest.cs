using UnityEngine;
using UnityEditor;

public class IsParserTest : BinaryParserTest<IsParser, IsToken> {
    public override string ParserSymbol { get { return "is"; } }
    public override bool SymbolRequiresTrailingSpace { get { return true; } }
}