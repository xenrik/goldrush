using NUnit.Framework;
using System.Collections.Generic;

public class MultiplicationParserTest : BinaryParserTest<MultiplicationParser,MultiplicationToken> {
    public override string ParserSymbol { get { return "*"; } }
}
